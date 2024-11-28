using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Victoralm.MAE.API.GraphQL.Mutations.Results;

namespace Victoralm.MAE.API.GraphQL.Subscriptions;

public partial class Subscription
{
    [Subscribe]
    public MedicResult MedicAdded([EventMessage] MedicResult medic) => medic;

    [SubscribeAndResolve]
    // Manually subscribing to an event with ITopicEventReceiver
    public ValueTask<ISourceStream<MedicResult>> MedicUpdated(Guid medicId, [Service] ITopicEventReceiver topicEventReceiver)
    {
        string topicName = $"{medicId}_{nameof(Subscription.MedicUpdated)}";
        return topicEventReceiver.SubscribeAsync<MedicResult>(topicName);
    }
}
