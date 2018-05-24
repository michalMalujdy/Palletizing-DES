using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TS.Common.Extensions;
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

            StepInsideNet(new StatesPath(), net, net.CurrentState, finishStateId);
            
            return ImportantPaths
                .Where(p => p.Status == StatesPathStatus.Successful)
                .OrderBy(p => p.StatesPathCollection.Count)
                .FirstOrDefault();
        }

        public StatesPath CheckBlocking()
        {
            ImportantPaths = new List<StatesPath>();
            var net = Mapper.Map<StatesNet>(StatesNet);
            var startState = net.AllStates.First();
            var finishState = net.AllStates.Last();
            net.CurrentState = startState;

            StepInsideNet(new StatesPath(), net, net.CurrentState, finishState.Id);

            return ImportantPaths
                .FirstOrDefault(p => p.Status == StatesPathStatus.Blocked);
        }

        private void StepInsideNet(StatesPath path, StatesNet net, State currentState, string finishStateId)
        {
            if (currentState.AvailableEvents.IsNullOrEmpty())
            {
                EndPath(path, currentState, StatesPathStatus.Blocked);
                return;
            }

            if (StateOccuredPreviously(path, currentState))
            {
                EndPath(path, currentState, StatesPathStatus.Unsuccessful);
                return;
            }
            
            if (currentState.Id.Equals(finishStateId))
            {
                EndPath(path, currentState, StatesPathStatus.Successful);
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

                StepInsideNet(iterationPath, net, nextState, finishStateId);
            }
        }

        private bool StateOccuredPreviously(StatesPath path, State currentState)
        {
            return path.StatesPathCollection
                .Select(node => node.State.Id)
                .Any(id => id.Equals(currentState.Id));
        }

        private void EndPath(StatesPath path, State currentState, StatesPathStatus pathStatus)
        {

            var pathCopy = Mapper.Map<StatesPath>(path);
            pathCopy.Status = pathStatus;
            pathCopy.StatesPathCollection.Add(new StatesPathNode() { State = currentState });
            ImportantPaths.Add(pathCopy);
        }
    }
}
