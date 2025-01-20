using System.Data.SqlClient;

namespace terrain
{
    public class Score
    {
        public Score(int  IdJoueur, DateTime date, int pt)
        {
            this.IdJoueur = IdJoueur;
            this.DateScore = date;
            this.Pt = pt;
        }
        public Score()
        {
        }
        public int IdJoueur {get;set;}
        public int Pt {get;set;}
        public DateTime DateScore {get;set;}


        public Score? getById (int idJoueur){
            MyConnection myConnection = new MyConnection();
            myConnection.OpenConnection();
            string query = "SELECT * FROM scoreJoueur WHERE idJoueur = " + idJoueur + " order by datescore desc limit 1";
            using (var cmd = myConnection.connection.CreateCommand())
            {
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            int ptT = reader.GetInt32(0);
                            int idJ = reader.GetInt32(1); 
                            DateTime  dateTime= reader.GetDateTime(2);
                            return new Score(idJ , dateTime ,ptT);
                        }
                    }
                }
            }
            myConnection.CloseConnection();
            return null;
        }
    }
}
