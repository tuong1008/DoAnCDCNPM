using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SignalrSqlDependency1.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace SignalrSqlDependency1.Repository{
    public class LenhDatRepository : ILenhDatRepository
    {
        private readonly IHubContext<SignalServer> _context;
        string connectionString = "";
        public LenhDatRepository(IConfiguration configuration,
                                    IHubContext<SignalServer> context)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }
        public List<LenhDat> GetAllLenhDats()
        {
            var lenhDats = new List<LenhDat>();

            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();

                SqlDependency.Start(connectionString);

                string commandText = "select id, maCP, ngayDat, loaiGD, loaiLenh, soLuong, giaDat, trangThaiLenh from dbo.LenhDats";

                SqlCommand cmd = new SqlCommand(commandText,conn);

                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange+=new OnChangeEventHandler(dbChangeNotification);

                var reader = cmd.ExecuteReader();

                while(reader.Read()){
                    var lenhDat = new LenhDat{
                        id = Convert.ToInt32(reader["id"]),
                        maCP = reader["maCP"].ToString(),
                        ngayDat = DateTime.Parse(reader["ngayDat"].ToString()),
                        loaiGD = reader["loaiGD"].ToString(),
                        loaiLenh = reader["loaiLenh"].ToString(),
                        soLuong = Convert.ToInt32(reader["soLuong"]),
                        giaDat = Single.Parse(reader["giaDat"].ToString()),
                        trangThaiLenh = reader["trangThaiLenh"].ToString()
                    };

                    lenhDats.Add(lenhDat);
                }                 

            }

            return lenhDats;
        }
        public string AddLenhDat(LenhDat lenhDat)
        {            
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();

                string commandText = "EXECUTE [dbo].[SP_KHOPLENH_LO] @maCP,@loaiGD,@soLuong,@giaDat";

                SqlCommand cmd = new SqlCommand(commandText,conn);
                // cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@giaDat", lenhDat.giaDat);
                cmd.Parameters.AddWithValue("@loaiGD", lenhDat.loaiGD);
                cmd.Parameters.AddWithValue("@maCP", lenhDat.maCP);
                cmd.Parameters.AddWithValue("@soLuong", lenhDat.soLuong);
                
                try{
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex){
                    return ex.Message;
                }
                

                
            }

            return "Thêm thành công";
        }

        private async void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            _context.Clients.All.SendAsync("refreshLenhDats");
        }
    }
}