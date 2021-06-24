using Domain.LuizaAuth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.LuizaAuth.Mappings
{
    public class RecoveryPasswordMap : IEntityTypeConfiguration<RecoveryPassword>
    {
        public void Configure(EntityTypeBuilder<RecoveryPassword> builder)
        {
            builder.Property(x => x.ID).IsRequired();

            builder.Property(x => x.AccountId).IsRequired();

            builder.Property(x => x.Created).IsRequired();
        }
    }
}
