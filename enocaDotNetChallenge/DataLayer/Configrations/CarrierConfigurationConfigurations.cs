using enocaDotNetChallenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Data.Configrations
{
    internal class CarrierConfigurationConfigurations : IEntityTypeConfiguration<Core.Models.CarrierConfigurations>
    {
        public void Configure(EntityTypeBuilder<Core.Models.CarrierConfigurations> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x=>x.CarrierMinDesi).IsRequired();
            builder.Property(x=>x.CarrierMaxDesi).IsRequired();
            builder.Property(x=>x.CarrierCost).IsRequired();
            builder.HasOne(x => x.Carriers)
            .WithMany(c => c.CarrierConfigurations)
            .HasForeignKey(x => x.CarrierId);
        }
    }
}
