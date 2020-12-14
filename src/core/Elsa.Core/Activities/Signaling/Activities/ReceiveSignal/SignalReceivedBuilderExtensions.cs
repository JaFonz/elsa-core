using System;
using Elsa.Activities.Signaling;
using Elsa.Builders;
using Elsa.Services.Models;

// ReSharper disable once CheckNamespace
namespace Elsa.Activities.ControlFlow
{
    public static class SignalReceivedBuilderExtensions
    {
        public static IActivityBuilder SignalReceived(this IBuilder builder, Action<ISetupActivity<SignalReceived>> setup) => builder.Then(setup);
        public static IActivityBuilder SignalReceived(this IBuilder builder, Func<ActivityExecutionContext, string> signal) => builder.SignalReceived(activity => activity.Set(x => x.Signal, signal));
        public static IActivityBuilder SignalReceived(this IBuilder builder, Func<string> signal) => builder.SignalReceived(activity => activity.Set(x => x.Signal, signal));
        public static IActivityBuilder SignalReceived(this IBuilder builder, string signal) => builder.SignalReceived(activity => activity.Set(x => x.Signal, signal));
    }
}