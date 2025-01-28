namespace WebMicroondas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoATabelaAquecimentosPreDefinidos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AquecimentoPreDBs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Alimento = c.String(nullable: false),
                        Tempo = c.Int(nullable: false),
                        Potencia = c.Int(),
                        MensagemDeAquecimento = c.String(nullable: false),
                        InstrucoesComplementares = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AquecimentoPreDBs");
        }
    }
}
