//-------------------------------------------------------------------------------
// <copyright file="TransitionDictionaryFacts.cs" company="Appccelerate">
//   Copyright (c) 2008-2017 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.AsyncMachine.Transitions
{
    using System;
    using Appccelerate.StateMachine.Machine;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;
    using Events = Appccelerate.StateMachine.Events;
    using States = Appccelerate.StateMachine.States;

    public class TransitionDictionaryFacts
    {
        private readonly IState<States, Events> state;

        private readonly Machine.Transitions.TransitionDictionary<States, Events> testee;

        public TransitionDictionaryFacts()
        {
            this.state = A.Fake<IState<States, Events>>();

            this.testee = new Machine.Transitions.TransitionDictionary<States, Events>(this.state);
        }

        [Fact]
        public void TransitionWhenTransitionIsAlreadyUsedForAnotherStateThenThrowException()
        {
            var transition = A.Fake<ITransition<States, Events>>();
            transition.Source = null;

            this.testee.Add(Events.A, transition);

            Action action = () => this.testee.Add(Events.B, transition);

            action
                .ShouldThrow<Exception>()
                .WithMessage(ExceptionMessages.TransitionDoesAlreadyExist(transition, this.state));
        }
    }
}