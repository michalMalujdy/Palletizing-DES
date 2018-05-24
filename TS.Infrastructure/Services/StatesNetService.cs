using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TS.Core.Enums;
using TS.Core.Json;
using TS.Core.Models;

namespace TS.Infrastructure.Services
{
    public class StatesNetService
    {
        public StatesNet StatesNet { get; set; }
        public List<StatesPath> ImportantPaths { get; set; }

        public void Initialize(StatesNetJson statesNetJson)
        {
            StatesNet = Mapper.Map<StatesNet>(statesNetJson);
            StatesNet.CurrentState = StatesNet.GetById(statesNetJson.StartStateId);
        }

        public void AriseEvent(string eventId)
        {
            StatesNet.PreviousStateId = StatesNet.CurrentState.Id;
            StatesNet.PreviousEventId = eventId;

            var nextStateId = StatesNet.CurrentState.AvaliableStatesIds[eventId];
            StatesNet.CurrentState = StatesNet.GetById(nextStateId);
        }

        public StatesPath FindPath(string startStateId, string finishStateId)
        {
            if (string.IsNullOrWhiteSpace(startStateId) || string.IsNullOrWhiteSpace(finishStateId))
            {
                throw new ArgumentException("Parameters cannot be null");
            }

            ImportantPaths = new List<StatesPath>();

            var net = Mapper.Map<StatesNet>(StatesNet);
            net.CurrentState = net.GetById(startStateId);

            PerformStep(new StatesPath(), net, net.CurrentState, finishStateId);
            
            return ImportantPaths
                .Where(p => p.Status == StatesPathStatus.Successful)
                .OrderBy(p => p.StatesPathCollection.Count)
                .FirstOrDefault();
        }

        private void PerformStep(StatesPath path, StatesNet net, State currentState, string finishStateId)
        {

            if (StateOccuredPreviously(path, currentState))
            {
                var pathCopy = Mapper.Map<StatesPath>(path);
                pathCopy.Status = StatesPathStatus.Unsuccessful;
                pathCopy.StatesPathCollection.Add(new StatesPathNode() { State = currentState });
                ImportantPaths.Add(pathCopy);
                return;
            }
            
            if (currentState.Id.Equals(finishStateId))
            {
                var pathCopy = Mapper.Map<StatesPath>(path);
                pathCopy.Status = StatesPathStatus.Successful;
                pathCopy.StatesPathCollection.Add(new StatesPathNode() { State = currentState });
                ImportantPaths.Add(pathCopy);
                return;
            }

            foreach (var availableEvent in currentState.AvailableEvents)
            {
                var currentNode = new StatesPathNode
                {
                    State = currentState,
                    LeavingEventId = availableEvent
                };

                var iterationPath = Mapper.Map<StatesPath>(path);
                iterationPath.StatesPathCollection.Add(currentNode);

                var nextStateId = currentState.AvaliableStatesIds[availableEvent];
                var nextState = net.GetById(nextStateId);

                PerformStep(iterationPath, net, nextState, finishStateId);
            }
        }

        private bool StateOccuredPreviously(StatesPath path, State currentState)
        {
            return path.StatesPathCollection
                .Select(node => node.State.Id)
                .Any(id => id.Equals(currentState.Id));
        }
    }
}
