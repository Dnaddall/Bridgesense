using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Bridgesense.Models.BridgesenseData;

namespace Bridgesense.Data
{
  public partial class BridgesenseDataContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public BridgesenseDataContext(DbContextOptions<BridgesenseDataContext> options):base(options)
    {
    }

    public BridgesenseDataContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Bridgesense.Models.BridgesenseData.UdpMessage>().HasNoKey();
        builder.Entity<Bridgesense.Models.BridgesenseData.Bridgelog>()
              .HasOne(i => i.Bridge)
              .WithMany(i => i.Bridgelogs)
              .HasForeignKey(i => i.bridgeid)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.Bridgestat>()
              .HasOne(i => i.Bridge)
              .WithMany(i => i.Bridgestats)
              .HasForeignKey(i => i.bridge_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.Sensor>()
              .HasOne(i => i.Bridge)
              .WithMany(i => i.Sensors)
              .HasForeignKey(i => i.bridge_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.SensorEvent>()
              .HasOne(i => i.Sensor)
              .WithMany(i => i.SensorEvents)
              .HasForeignKey(i => i.sensor_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.SensorEventCount>()
              .HasOne(i => i.Bridge)
              .WithMany(i => i.SensorEventCounts)
              .HasForeignKey(i => i.bridge_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.SensorEventCount>()
              .HasOne(i => i.Sensor)
              .WithMany(i => i.SensorEventCounts)
              .HasForeignKey(i => i.sensor_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.SensorStatus>()
              .HasOne(i => i.Sensor)
              .WithMany(i => i.SensorStatuses)
              .HasForeignKey(i => i.sensor_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<Bridgesense.Models.BridgesenseData.SensorstatsLight>()
              .HasOne(i => i.Sensor)
              .WithMany(i => i.SensorstatsLights)
              .HasForeignKey(i => i.sensor_id)
              .HasPrincipalKey(i => i.id);

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridge>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('bridges_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridge>()
              .Property(p => p.vild_code)
              .HasDefaultValueSql("''::text");

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridge>()
              .Property(p => p.owner)
              .HasDefaultValueSql("''::text");

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridge>()
              .Property(p => p.manager)
              .HasDefaultValueSql("''::text");

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridge>()
              .Property(p => p.isrs)
              .HasDefaultValueSql("''::text");

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridgelog>()
              .Property(p => p.duration)
              .HasDefaultValueSql("'-1'::integer").ValueGeneratedNever();

        builder.Entity<Bridgesense.Models.BridgesenseData.Bridgestat>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('bridgestats_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.LastLuxEvent>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('last_lux_events_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.Sensor>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('sensors_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.SensorEvent>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('sensor_events_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.SensorStatus>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('sensor_status_id_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.UdpMessage>()
              .Property(p => p.logid)
              .HasDefaultValueSql("nextval('udp_logid_seq'::regclass)");

        builder.Entity<Bridgesense.Models.BridgesenseData.User>()
              .Property(p => p.is_active)
              .HasDefaultValueSql("false");

        builder.Entity<Bridgesense.Models.BridgesenseData.User>()
              .Property(p => p.is_admin)
              .HasDefaultValueSql("false");

        builder.Entity<Bridgesense.Models.BridgesenseData.User>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('users_id_seq'::regclass)");

        this.OnModelBuilding(builder);
    }


    public DbSet<Bridgesense.Models.BridgesenseData.Bridge> Bridges
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.Bridgelog> Bridgelogs
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.Bridgestat> Bridgestats
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.LastLuxEvent> LastLuxEvents
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.Sensor> Sensors
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.SensorEvent> SensorEvents
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.SensorEventCount> SensorEventCounts
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.SensorStatus> SensorStatuses
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.SensorstatsLight> SensorstatsLights
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.Test> Tests
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.UdpMessage> UdpMessages
    {
      get;
      set;
    }

    public DbSet<Bridgesense.Models.BridgesenseData.User> Users
    {
      get;
      set;
    }
  }
}
