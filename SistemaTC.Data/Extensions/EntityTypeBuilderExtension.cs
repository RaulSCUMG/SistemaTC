using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaTC.Data.Extensions;
public static class EntityTypeBuilderExtension
{
    public static void AddAuditableFields(this EntityTypeBuilder entity)
    {
        entity.Property("Created").IsRequired();
        entity.Property("CreatedBy").IsRequired().HasMaxLength(100);
        entity.Property("Updated").IsRequired(false);
        entity.Property("UpdatedBy").IsRequired(false).HasMaxLength(100);
    }
}
