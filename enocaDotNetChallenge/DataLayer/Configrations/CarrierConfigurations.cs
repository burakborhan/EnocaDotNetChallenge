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
    internal class CarrierConfigurations : IEntityTypeConfiguration<Carriers>
    {
        public void Configure(EntityTypeBuilder<Carriers> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.CarrierName).IsRequired();
            builder.Property(x=>x.CarrierPlusDesiCost).IsRequired();
            //builder.HasMany(x => x.CarrierConfigurations)
            //.WithOne(c => c.Carriers)
            //.HasForeignKey(c => c.Id);
            //builder.HasMany(x => x.Orders)
            //.WithOne(c => c.carriers)
            //.HasForeignKey(c => c.Id);
        }
    }
}
