﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Titles.Api.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TitlesEntities : DbContext
    {
        public TitlesEntities()
            : base("name=TitlesEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<OtherName> OtherNames { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<StoryLine> StoryLines { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<TitleGenre> TitleGenres { get; set; }
        public virtual DbSet<TitleParticipant> TitleParticipants { get; set; }
    }
}
