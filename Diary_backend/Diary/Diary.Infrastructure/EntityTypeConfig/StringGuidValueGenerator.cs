using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Diary.Infrastructure.EntityTypeConfig
{
    internal sealed class StringGuidValueGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}
