using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    public class rundownlistdetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public rundownlistdetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetRundownlistDetails(int rundownid)
        {
            object outputresponse = null;
            DataTable dt = null;
            string cs = _configuration.GetConnectionString("SqlServer1");
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetRundownlistDetails";

                    if (rundownid > 0)
                    {
                        cmd.Parameters.Add("@rundownid", SqlDbType.Int).Value = rundownid;
                    }
                    else
                    {
                        cmd.Parameters.Add("@rundownid", SqlDbType.Int).Value = 0;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                outputresponse = dt;

                return new JsonResult(outputresponse);
            }
            else
            {
                outputresponse = "Data is not found";
                return new JsonResult(outputresponse);
            }
        }

        [HttpDelete]
        public JsonResult DeleteRundownlistDetailsById(int id, int rundownId)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            int Results = 0;
            object outputresponse = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spDeleteRundounlistRecord";

                    if (id > 0)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    }
                    else
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                    }
                    if (rundownId > 0)
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = rundownId;
                    }
                    else
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = 0;
                    }
                    con.Open();
                    Results = cmd.ExecuteNonQuery();
                }
            }

            if (Results != 0)
            {
                outputresponse = "Record deleted successfully";
                return new JsonResult(outputresponse);
            }
            else
            {
                outputresponse = "Record not deleted";
                return new JsonResult(outputresponse);
            }
        }

        [HttpPut]
        public JsonResult UpdateRundownlistDetailsRecord([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");

            int Results = 0;
            object outputresponse = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spUpdateRundownlistDetails";
                    if (objRundownlistDetailsDTO.id > 0)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = objRundownlistDetailsDTO.id;
                    }
                    if (objRundownlistDetailsDTO.rundownId > 0)
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = objRundownlistDetailsDTO.rundownId;
                    }
                    else
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = null;
                    }

                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.status))
                    {
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.status;

                    }
                    else
                    {
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugName))
                    {
                        cmd.Parameters.Add("@slugName", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.slugName;
                    }
                    else
                    {
                        cmd.Parameters.Add("@slugName", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugType))
                    {
                        cmd.Parameters.Add("@slugType", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.slugType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@slugType", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.reporter))
                    {
                        cmd.Parameters.Add("@reporter", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.reporter;
                    }
                    else
                    {
                        cmd.Parameters.Add("@reporter", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.assignTo))
                    {
                        cmd.Parameters.Add("@assignTo", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.assignTo;
                    }
                    else
                    {
                        cmd.Parameters.Add("@assignTo", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.acceptedBy))
                    {
                        cmd.Parameters.Add("@acceptedBy", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.acceptedBy;
                    }
                    else
                    {
                        cmd.Parameters.Add("@acceptedBy", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.createDate))
                    {
                        cmd.Parameters.Add("@createDate", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.createDate;
                    }
                    else
                    {
                        cmd.Parameters.Add("@createDate", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.gfx_attachment))
                    {
                        cmd.Parameters.Add("@gfx_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.gfx_attachment;
                    }
                    else
                    {
                        cmd.Parameters.Add("@gfx_attachment", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.video_attachment))
                    {
                        cmd.Parameters.Add("@video_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.video_attachment;
                    }
                    else
                    {
                        cmd.Parameters.Add("@video_attachment", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.remarks))
                    {
                        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.remarks;
                    }
                    else
                    {
                        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = null;
                    }

                    con.Open();
                    Results = cmd.ExecuteNonQuery();
                }
            }

            if (Results != 0)
            {
                outputresponse = "Record updated successfully";
                return new JsonResult(outputresponse);
            }
            else
            {
                outputresponse = "Record not updated";
                return new JsonResult(outputresponse);
            }

        }
        [HttpPut]
        public JsonResult InsertRundownlistDetailsRecord([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");

            int Results = 0;
            object outputresponse = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spInsertRundownListDetails";

                    // cmd.Parameters.Add("@rundownid", SqlDbType.Int).Value = objRundownlistDetailsInsertDTO.rundownId;
                    // cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.status;
                    // cmd.Parameters.Add("@slugName", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.slugName;
                    // cmd.Parameters.Add("@slugtype", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.slugType;
                    // cmd.Parameters.Add("@reporter", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.reporter;
                    // cmd.Parameters.Add("@assignTo", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.assignTo;
                    // cmd.Parameters.Add("@acceptedBy", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.acceptedBy;
                    // cmd.Parameters.Add("@createDate", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.createDate;
                    // cmd.Parameters.Add("@gfx_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.gfx_attachment;
                    // cmd.Parameters.Add("@video_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.video_attachment;
                    // cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = objRundownlistDetailsInsertDTO.remarks;

                    //  if(objRundownlistDetailsDTO.id>0)
                    // {
                    //     cmd.Parameters.Add("@id", SqlDbType.Int).Value = objRundownlistDetailsDTO.id;
                    // }
                    if (objRundownlistDetailsDTO.rundownId > 0)
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = objRundownlistDetailsDTO.rundownId;
                    }
                    else
                    {
                        cmd.Parameters.Add("@rundownId", SqlDbType.Int).Value = null;
                    }

                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.status))
                    {
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.status;

                    }
                    else
                    {
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugName))
                    {
                        cmd.Parameters.Add("@slugName", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.slugName;
                    }
                    else
                    {
                        cmd.Parameters.Add("@slugName", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugType))
                    {
                        cmd.Parameters.Add("@slugType", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.slugType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@slugType", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.reporter))
                    {
                        cmd.Parameters.Add("@reporter", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.reporter;
                    }
                    else
                    {
                        cmd.Parameters.Add("@reporter", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.assignTo))
                    {
                        cmd.Parameters.Add("@assignTo", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.assignTo;
                    }
                    else
                    {
                        cmd.Parameters.Add("@assignTo", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.acceptedBy))
                    {
                        cmd.Parameters.Add("@acceptedBy", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.acceptedBy;
                    }
                    else
                    {
                        cmd.Parameters.Add("@acceptedBy", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.createDate))
                    {
                        cmd.Parameters.Add("@createDate", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.createDate;
                    }
                    else
                    {
                        cmd.Parameters.Add("@createDate", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.gfx_attachment))
                    {
                        cmd.Parameters.Add("@gfx_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.gfx_attachment;
                    }
                    else
                    {
                        cmd.Parameters.Add("@gfx_attachment", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.video_attachment))
                    {
                        cmd.Parameters.Add("@video_attachment", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.video_attachment;
                    }
                    else
                    {
                        cmd.Parameters.Add("@video_attachment", SqlDbType.VarChar).Value = null;
                    }
                    if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.remarks))
                    {
                        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = objRundownlistDetailsDTO.remarks;
                    }
                    else
                    {
                        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = null;
                    }

                    con.Open();
                    Results = cmd.ExecuteNonQuery();
                }
            }

            if (Results != 0)
            {
                outputresponse = "Record inserted successfully";
                return new JsonResult(outputresponse);
            }
            else
            {
                outputresponse = "Record not inserted";
                return new JsonResult(outputresponse);
            }

        }

    }
}
