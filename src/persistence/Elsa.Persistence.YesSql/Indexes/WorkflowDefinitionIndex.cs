using Elsa.Data;
using Elsa.Models;
using Elsa.Persistence.YesSql.Documents;
using YesSql.Indexes;

namespace Elsa.Persistence.YesSql.Indexes
{
    public class WorkflowDefinitionIndex : MapIndex
    {
        public string? TenantId { get; set; }
        public string EntityId { get; set; } = default!;
        public string DefinitionVersionId { get; set; } = default!;
        public int Version { get; set; }
        public bool IsLatest { get; set; }
        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class WorkflowDefinitionIndexProvider : IndexProvider<WorkflowDefinitionDocument>
    {
        public WorkflowDefinitionIndexProvider() => CollectionName = CollectionNames.WorkflowDefinitions;

        public override void Describe(DescribeContext<WorkflowDefinitionDocument> context)
        {
            context.For<WorkflowDefinitionIndex>()
                .Map(
                    workflowDefinition => new WorkflowDefinitionIndex
                    {
                        EntityId = workflowDefinition.EntityId,
                        TenantId = workflowDefinition.TenantId,
                        DefinitionVersionId = workflowDefinition.DefinitionVersionId,
                        Version = workflowDefinition.Version,
                        IsPublished = workflowDefinition.IsPublished,
                        IsLatest = workflowDefinition.IsLatest,
                        IsEnabled = workflowDefinition.IsEnabled
                    }
                );
        }
    }
}
