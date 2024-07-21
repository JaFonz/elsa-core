using Elsa.Mediator.Contracts;
using Elsa.Webhooks.Models;
using Elsa.Workflows.Runtime.Notifications;
using JetBrains.Annotations;
using WebhooksCore;

namespace Elsa.Webhooks.Handlers;

/// Handles the <see cref="RunTaskRequest"/> notification and asynchronously invokes all registered webhook endpoints.
[UsedImplicitly]
public class RunTaskHandler(IWebhookEventBroadcaster webhookDispatcher) : INotificationHandler<RunTaskRequest>
{
    /// <inheritdoc />
    public async Task HandleAsync(RunTaskRequest notification, CancellationToken cancellationToken)
    {
        var activityExecutionContext = notification.ActivityExecutionContext;
        var workflowExecutionContext = activityExecutionContext.WorkflowExecutionContext;
        var workflowInstanceId = workflowExecutionContext.Id;
        var correlationId = workflowExecutionContext.CorrelationId;
        var workflow = workflowExecutionContext.Workflow;
        var workflowDefinitionId = workflow.Identity.DefinitionId;
        var workflowName = workflow.WorkflowMetadata.Name;
        
        var payload = new RunTaskWebhook(
            workflowInstanceId,
            workflowDefinitionId,
            workflowName,
            correlationId,
            notification.TaskId, 
            notification.TaskName, 
            notification.TaskPayload
        );
        
        var webhookEvent = new NewWebhookEvent("RunTask", payload);
        await webhookDispatcher.BroadcastAsync(webhookEvent, cancellationToken);
    }
}