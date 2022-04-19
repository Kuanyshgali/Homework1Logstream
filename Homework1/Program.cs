using Homework1.EF;
using System.Data.SqlClient;

static void EFTest()
{
    TeamContext db = new TeamContext();
    db.CreateDbIfNotExist();
    //*********************************************************************************************************************************
    var PlayerList = from player in db.Player
                     join team in db.Teams on player.idTeam equals team.Id
                     orderby team.Title, player.Cost descending
                     select new
                     {
                         Name = player.Name,
                         Team = team.Title,
                         Cost = player.Cost
                     };
    foreach (var player in PlayerList)
        Console.WriteLine(player.Name + " " + player.Team + " " + player.Cost);
    Console.WriteLine();
    //*********************************************************************************************************************************
    var Successful = from player in db.Player
                     orderby player.Cost / (player.Age - 18) descending
                     select new { Name = player.Name, Age = player.Age, Cost = player.Cost };

    foreach (var player in Successful)
        Console.WriteLine(player.Name + " " + player.Cost + " " + player.Age);
    Console.WriteLine();
    //*********************************************************************************************************************************
    var LessAverageCost = from player in db.Player
                          where player.Cost <
                          (from player1 in db.Player
                           select player1.Cost).Average()
                          select new { Name = player.Name, Cost = player.Cost };
    foreach (var player in LessAverageCost)
        Console.WriteLine(player.Name + " " + player.Cost);
    Console.WriteLine();
    //*********************************************************************************************************************************
    var TeamAverageAge = from team in db.Teams
                         join player in db.Player on team.Id equals player.idTeam into all
                         from res in all
                         group res by res.team.Title into newTable
                         select new
                         {
                             Title = newTable.Key,
                             Average_cost = newTable.Average(a => a.Age)
                         };
    foreach (var player in TeamAverageAge)
        Console.WriteLine(player.Title + " " + player.Average_cost);
    Console.WriteLine();
    //*********************************************************************************************************************************
    var UpdateMostSalaryPlayer = db.Player
                                 .Where(p => p.Cost == (from player1 in db.Player
                                                        select player1.Cost).Max())
                                 .FirstOrDefault();

    UpdateMostSalaryPlayer.Cost = UpdateMostSalaryPlayer.Cost * 0.9;
    db.SaveChanges();

    Console.WriteLine("Name = {0} | Cost after edit = {1}", UpdateMostSalaryPlayer.Name, UpdateMostSalaryPlayer.Cost);
    Console.WriteLine();
    //*********************************************************************************************************************************
    var DeleteMinSalaryPlayer = db.Player
                                 .Where(p => p.Cost == (from player1 in db.Player
                                                        select player1.Cost).Min())
                                 .FirstOrDefault();

    db.Remove(DeleteMinSalaryPlayer);
    db.SaveChanges();

    Console.WriteLine("Name = {0} | Her cost = {1}", DeleteMinSalaryPlayer.Name, DeleteMinSalaryPlayer.Cost);
    Console.WriteLine();
    //*********************************************************************************************************************************
}

static void AdoNetTest()
{
    TeamContext db = new TeamContext();
    db.CreateDbIfNotExist();
    var connectionString = "Server=DESKTOP-AE5PD09; Database=TeamDB; Trusted_Connection=True;";

    var connection = new SqlConnection(connectionString);
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText =
    "SELECT tm.Id, SUM (Cost) as ssum FROM dbo.Player as pl JOIN dbo.Teams as tm ON(pl.idTeam = tm.Id) GROUP BY tm.Id ORDER BY SUM(Cost)";
    var reader = command.ExecuteReader();
    reader.Read();
    var teamId = reader.GetInt32(reader.GetOrdinal("Id"));
    reader.Close();

    var insert = $"INSERT INTO Player (Name,Age,Cost,idTeam)" +
                          $"VALUES('Khilan', 25, '20', {teamId});";
    Console.WriteLine(insert);
    var command2 = new SqlCommand(insert, connection);
    SqlDataReader reader2 = command2.ExecuteReader();
    reader2.Read();
    reader2.Close();
    connection.Close();
}

EFTest();
AdoNetTest();