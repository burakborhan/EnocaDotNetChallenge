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
    internal class OrderConfigurations : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.OrderCarrierCost).IsRequired();
            builder.Property(x => x.OrderDesi).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.HasOne(x => x.carriers).WithMany(x => x.Orders).HasForeignKey(x=> x.CarrierId);

        }
    }
}
