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
using System.Collections;

namespace ApiServer.Controllers
{

    // [Route("[controller]")]
    public class rundownController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public rundownController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getrundownlist")]
        public JsonResult getrundownlist()
        {
            dynamic data = null;
            object outputresponse = null;
            DataTable dt = null;
            string cs = _configuration.GetConnectionString("SqlServer1");

            string query = "select rundownId" + "," + "rundownName" + "," + "convert(varchar, date, 23) as date" + "," + "convert(varchar, time, 8) as time" + "," +
                "rundownStatus" + "," + "rundownRemarks" + "," + "createdBy" + "," + "convert(varchar, creationTime, 8) as creationTime from rundownlist order by rundownName" + "";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    outputresponse = dt;

                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Data is not found";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);

            }
            return data;
        }

        [HttpDelete("api/deleterundownlist")]
        public JsonResult deleterundownlist(int rundownId)
        {
            dynamic data = null;
            string cs = _configuration.GetConnectionString("SqlServer1");
            int Results = 0;
            object outputresponse = null;
            string query = "delete from rundownlist where rundownId=" + rundownId + "";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Record deleted successfully";
                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Record not deleted";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;
        }

        [HttpPut("api/updaterundownlist")]
        public JsonResult UpdateRundownlist([FromBody] RundownDTOO objRundownlistDTO)
        {



            string cs = _configuration.GetConnectionString("SqlServer1");
            dynamic data = null;
            int Results = 0;
            object outputresponse = null;


            string s = "";
            string query = "update rundownlist set ";


            if (!string.IsNullOrEmpty(objRundownlistDTO.rundownName))
            {
                if (s == "")
                    s = " rundownName='" + objRundownlistDTO.rundownName + "'";
                else s += ",rundownName='" + objRundownlistDTO.rundownName + "'"; ;
            }
            if (!string.IsNullOrEmpty(objRundownlistDTO.date))
            {
                if (s == "")
                    s = "date='" + objRundownlistDTO.date + "'";
                else s += ",date='" + objRundownlistDTO.date + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDTO.time))
            {
                if (s == "")
                    s = " time='" + objRundownlistDTO.time + "'";
                else s += ",time='" + objRundownlistDTO.time + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDTO.rundownStatus))
            {
                if (s == "")
                    s = " rundownStatus='" + objRundownlistDTO.rundownStatus + "'";
                else s += ", rundownStatus='" + objRundownlistDTO.rundownStatus + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDTO.rundownRemarks))
            {
                if (s == "")
                    s = "rundownRemarks='" + objRundownlistDTO.rundownRemarks + "'";
                else s += ", rundownRemarks='" + objRundownlistDTO.rundownRemarks + "'";
            }

            if (!string.IsNullOrEmpty(objRundownlistDTO.createdBy))
            {
                if (s == "")
                    s = "createdBy='" + objRundownlistDTO.createdBy + "'";
                else s += ", createdBy='" + objRundownlistDTO.createdBy + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDTO.creationTime))
            {
                if (s == "")
                    s = "creationTime='" + objRundownlistDTO.creationTime + "'";
                else s += ", creationTime='" + objRundownlistDTO.creationTime + "'";
            }


            try
            {

                if (s != "")
                {
                    query += s + " where rundownId='" + objRundownlistDTO.rundownId + "'";

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            Results = cmd.ExecuteNonQuery();
                        }
                    }
                    if (Results != 0)
                    {
                        outputresponse = "Record updated successfully";
                        data = new JsonResult(outputresponse);
                    }
                    else
                    {
                        outputresponse = "Record not updated";
                        data = new JsonResult(outputresponse);
                    }

                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;


        }

        [HttpPut("api/insertrundownlist")]

        public JsonResult InsertRundownlist([FromBody] RundownDTOO objRundownlistDTO)
        {    
            string cs = _configuration.GetConnectionString("SqlServer1");

            dynamic data = null;
            int Results = 0;
            object outputresponse = null;
            string query = "insert into rundownlist values('" + objRundownlistDTO.rundownName + "','" + objRundownlistDTO.date + "','" + objRundownlistDTO.time +
            "','" + objRundownlistDTO.rundownStatus + "','" + objRundownlistDTO.rundownRemarks + "','" + objRundownlistDTO.createdBy + "','" + objRundownlistDTO.creationTime + "')";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Record Inserted successfully";
                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Record not Inserted";
                    data = new JsonResult(outputresponse);

                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;
        }


        [HttpPut("api/insertduplicaterundownlist")]

        public JsonResult InsertDuplicateRundownlist([FromBody] RundownDTOO objRundownlistDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            object obj;

            dynamic data = null;
            dynamic data1 = null;
            int Results = 0;
            object outputresponse = null;
            string query = "insert into rundownlist values('" + objRundownlistDTO.rundownName + "','" + objRundownlistDTO.date + "','" + objRundownlistDTO.time +
            "','" + objRundownlistDTO.rundownStatus + "','" + objRundownlistDTO.rundownRemarks + "','" + objRundownlistDTO.createdBy + "','" + objRundownlistDTO.creationTime + "')";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Duplicate Record Inserted successfully";
                    data = new JsonResult(outputresponse);

                    DataTable dt1 = null;

                    string query1 = "select id" + "," + "rundownId" + "," + "status" + "," + "slugName" + "," + "slugType" + "," + "reporter" + "," + "assignTo" + "," +
                "acceptedBy" + "," + "convert(varchar, createDate, 23) as createDate" + "," + "gfx_attachment" + "," + "video_attachment" + "," + "remarks" + "," + "story_editor"+ ","+"roleid from RundownlistDetails where rundownid=" + objRundownlistDTO.rundownId + "";
                    string query2 = "SELECT rundownid FROM rundownlist WHERE rundownid = (SELECT MAX(rundownid) FROM rundownlist)";

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(query2, con))
                        {
                            con.Open();
                            obj = cmd.ExecuteScalar();

                        }
                        // rundownid= new JsonResult(dt);
                    }
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(query1, con))
                        {

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            dt1 = new DataTable();
                            da.Fill(dt1);

                        }
                        data1 = new JsonResult(dt1);
                    }
                    foreach (DataRow row in dt1.Rows)
                    {
                        //insert into RundownlistDetails values ((SELECT MAX(id)+1 FROM RundownlistDetails),"
                        string querystr = "insert into RundownlistDetails values ((SELECT MAX(id)+1 FROM RundownlistDetails),"
                        + obj + ",'" + row["status"] + "','" + row["slugName"]+ "','" + row["slugType"] + "','" + row["reporter"] + "','" + row["assignTo"] + "','" 
                        + row["acceptedBy"] + "','" + row["createDate"] +"','" + row["gfx_attachment"] + "','" + row["video_attachment"] + "','" + row["remarks"]
                       + "','" + row["story_editor"] +"','" + row["roleid"] +"')";

                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            using (SqlCommand cmd = new SqlCommand(querystr, con))
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }


                    //return data;
                }
                else
                {
                    outputresponse = "Duplicate Record not Inserted";
                    data = new JsonResult(outputresponse);

                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            //return rundownid;
            return data;




        }

        [HttpPut("api/cutpasterowrundownlist")]
        public JsonResult cutpasterowrundownlist([FromBody] List<RundownlistDetail> lstRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            object obj;

            dynamic data = null;
            //dynamic data1 = null;
            int Results = 0;
            int targetID = lstRundownlistDetailsDTO[0].targetID;
            object outputresponse = null;
            string queryUpdate = string.Empty;
            string cutrow = string.Empty;
            string pasterow = string.Empty;
            bool Isdelete = true;

            foreach (var item in lstRundownlistDetailsDTO)
            {
                if (targetID > item.id)
                {
                    queryUpdate = "Update rundownlistdetails set id = id + 1 where id > " + targetID + "";
                    targetID = targetID + 1;
                    cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + "";
                    pasterow = "insert into rundownlistdetails values(" + targetID + "," + item.rundownId + ",'" + item.status + "','" + item.slugName + "','"
                    + item.slugType + "','" + item.reporter + "','" + item.assignTo + "','" + item.acceptedBy + "','" +
                    item.createDate + "','" + item.gfx_attachment + "','" + item.video_attachment + "','" + item.remarks + "','" + item.story_editor +"'," + item.roleid +")";
                }
                else
                {
                    if (Isdelete)
                    {
                        cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + "";
                        Isdelete = false;
                    }
                    else
                    {
                        cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + " + 1" + "";
                    }
                    queryUpdate = "Update rundownlistdetails set id = id + 1 where id > " + targetID + "";
                    targetID = targetID + 1;

                    pasterow = "insert into rundownlistdetails values(" + targetID + "," + item.rundownId + ",'" + item.status + "','" + item.slugName + "','"
                    + item.slugType + "','" + item.reporter + "','" + item.assignTo + "','" + item.acceptedBy + "','" +
                    item.createDate + "','" + item.gfx_attachment + "','" + item.video_attachment + "','" + item.remarks + "','" + item.story_editor + "'," + item.roleid +")";
                }
                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(queryUpdate + cutrow + pasterow, con))
                        {

                            con.Open();
                            Results = cmd.ExecuteNonQuery();
                        }
                    }
                    if (Results != 0)
                    {
                        outputresponse = "Row Cut Pasted successfully";
                        data = new JsonResult(outputresponse);


                    }
                    else
                    {
                        outputresponse = "Row can't Cut Pasted ";
                        data = new JsonResult(outputresponse);

                    }
                }
                catch (Exception ex)
                {
                    string[] errormsg = ex.StackTrace.Split(':');
                    string msg = errormsg[2];
                    WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                    data = new JsonResult(ex.Message);
                }

            }
            // if (Results != 0)
            // {
            //     outputresponse = "Row Cut Pasted successfully";
            //     data = new JsonResult(outputresponse);


            // }
            // else
            // {
            //     outputresponse = "Row can't Cut Pasted ";
            //     data = new JsonResult(outputresponse);

            // }

            //return rundownid;
            return data;




        }

        [HttpPut("api/dragdroprowrundownlist")]
        public JsonResult dragdroprowrundownlist([FromBody] List<RundownlistDetail> lstRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            object obj;

            dynamic data = null;
            //dynamic data1 = null;
            int Results = 0;
            int targetID = lstRundownlistDetailsDTO[0].targetID;
            object outputresponse = null;
            string queryUpdate = string.Empty;
            string cutrow = string.Empty;
            string pasterow = string.Empty;
            bool Isdelete = true;

            foreach (var item in lstRundownlistDetailsDTO)
            {
                if (targetID > item.id)
                {
                    queryUpdate = "Update rundownlistdetails set id = id + 1 where id > " + targetID + "";
                    targetID = targetID + 1;
                    cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + "";
                    pasterow = "insert into rundownlistdetails values(" + targetID + "," + item.rundownId + ",'" + item.status + "','" + item.slugName + "','"
                    + item.slugType + "','" + item.reporter + "','" + item.assignTo + "','" + item.acceptedBy + "','" +
                    item.createDate + "','" + item.gfx_attachment + "','" + item.video_attachment + "','" + item.remarks + "','" + item.story_editor +"'," + item.roleid +")";
                }
                else
                {
                    if (Isdelete)
                    {
                        cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + "";
                        Isdelete = false;
                    }
                    else
                    {
                        cutrow = "delete from rundownlistdetails where rundownId=" + item.rundownId + "and id=" + item.id + " + 1" + "";
                    }
                    queryUpdate = "Update rundownlistdetails set id = id + 1 where id > " + targetID + "";
                    targetID = targetID + 1;

                    pasterow = "insert into rundownlistdetails values(" + targetID + "," + item.rundownId + ",'" + item.status + "','" + item.slugName + "','"
                    + item.slugType + "','" + item.reporter + "','" + item.assignTo + "','" + item.acceptedBy + "','" +
                    item.createDate + "','" + item.gfx_attachment + "','" + item.video_attachment + "','" + item.remarks + "','" + item.story_editor + "'," + item.roleid+")";
                }


                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(queryUpdate + cutrow + pasterow, con))
                        {

                            con.Open();
                            Results = cmd.ExecuteNonQuery();
                        }
                    }
                    if (Results != 0)
                    {
                        outputresponse = "drag drop successfully";
                        data = new JsonResult(outputresponse);
                    }
                    else
                    {
                        outputresponse = "drag drop Failed  ";
                        data = new JsonResult(outputresponse);

                    }
                }
                catch (Exception ex)
                {
                    string[] errormsg = ex.StackTrace.Split(':');
                    string msg = errormsg[2];
                    WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                    data = new JsonResult(ex.Message);
                }

            }
            // if (Results != 0)
            // {
            //     outputresponse = "drag drop successfully";
            //     data = new JsonResult(outputresponse);
            // }
            // else
            // {
            //     outputresponse = "drag drop Failed  ";
            //     data = new JsonResult(outputresponse);

            // }
            //return rundownid;
            return data;
        }

        [HttpPut("api/rowinsertrundownlistdetails")]
        public JsonResult rowinsertrundownlist([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");

            dynamic data = null;
            int Results = 0;
            object outputresponse = null;
            //"Update rundownlistdetails set id = id + 1 where id > " + id + "";

            //string queryUpdate = "Update rundownlistdetails set id = id+1 where rundownid =" + objRundownlistDetailsDTO.rundownId + "and id>" + objRundownlistDetailsDTO.targetID + "";
            string queryUpdate="Update rundownlistdetails set id = id + 1 where id > " + objRundownlistDetailsDTO.targetID + "";

            int id = objRundownlistDetailsDTO.targetID + 1;

            string queryInsert = "insert into rundownlistdetails values(" + id + "," + objRundownlistDetailsDTO.rundownId + ",'" + objRundownlistDetailsDTO.status + "','" + objRundownlistDetailsDTO.slugName + "','"
            + objRundownlistDetailsDTO.slugType + "','" + objRundownlistDetailsDTO.reporter + "','" + objRundownlistDetailsDTO.assignTo + "','" + objRundownlistDetailsDTO.acceptedBy + "','" +
            objRundownlistDetailsDTO.createDate + "','" + objRundownlistDetailsDTO.gfx_attachment + "','" + objRundownlistDetailsDTO.video_attachment + "','" + objRundownlistDetailsDTO.remarks + "','" + objRundownlistDetailsDTO.story_editor +"'," + objRundownlistDetailsDTO.roleid+ ")";



            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(queryUpdate + queryInsert, con))
                    {

                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Row Inserted successfully";
                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Row not Inserted";
                    data = new JsonResult(outputresponse);

                }
            }

            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;
        }

        // [HttpPut("api/storyupdaterundownlistdetails")]
        // public JsonResult storyupdaterundownlistdetails([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        // {
        //     string cs = _configuration.GetConnectionString("SqlServer1");

        //     dynamic data = null;
        //     int Results = 0;
        //     object outputresponse = null;         





        //     // string query = "update RundownlistDetails set Story_Editor='"+objRundownlistDetailsDTO.Story_Editor+"' where id="+
        //     // objRundownlistDetailsDTO.id+"and rundownId="+objRundownlistDetailsDTO.rundownId+"";

        //     // try
        //     // {
        //     //     using (SqlConnection con = new SqlConnection(cs))
        //     //     {
        //     //         using (SqlCommand cmd = new SqlCommand(query, con))
        //     //         {

        //     //             con.Open();
        //     //             Results = cmd.ExecuteNonQuery();
        //     //         }
        //     //     }

        //     //     if (Results != 0)
        //     //     {
        //     //         outputresponse = "Story Updated successfully";
        //     //         data = new JsonResult(outputresponse);
        //     //     }
        //     //     else
        //     //     {
        //     //         outputresponse = "Story not Updated";
        //     //         data = new JsonResult(outputresponse);

        //     //     }
        //     // }
        //     // catch (Exception ex)
        //     // {
        //     //     System.Console.WriteLine(ex.ToString());
        //     // }
        //     // return data;
        // }

        [HttpPut("api/copypasterundownlistdetails")]
        public JsonResult copyrowrundownlistdetails([FromBody] List<RundownlistDetail> lstRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            object obj;

            dynamic data = null;
            //dynamic data1 = null;
            int Results = 0;
            int id = lstRundownlistDetailsDTO[0].targetID;
            object outputresponse = null;

            foreach (var item in lstRundownlistDetailsDTO)
            {


                string queryUpdate = "Update rundownlistdetails set id = id + 1 where id > " + id + "";
                id = id + 1;
                string pasterow = "insert into rundownlistdetails values(" + id + "," + item.rundownId + ",'" + item.status + "','" + item.slugName + "','"
                + item.slugType + "','" + item.reporter + "','" + item.assignTo + "','" + item.acceptedBy + "','" +
                item.createDate + "','" + item.gfx_attachment + "','" + item.video_attachment + "','" + item.remarks + "','" + item.story_editor + "'," + item.roleid +")";

                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(queryUpdate + pasterow, con))
                        {

                            con.Open();
                            Results = cmd.ExecuteNonQuery();
                        }
                    }
                    if (Results != 0)
                    {
                        outputresponse = "Copy Pasted successfully";
                        data = new JsonResult(outputresponse);
                    }
                    else
                    {
                        outputresponse = "Copy Paste failed ";
                        data = new JsonResult(outputresponse);
                    }
                }
                catch (Exception ex)
                {
                    string[] errormsg = ex.StackTrace.Split(':');
                    string msg = errormsg[2];
                    WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                    data = new JsonResult(ex.Message);
                }

            }
            // if (Results != 0)
            // {
            //     outputresponse = "Copy Pasted successfully";
            //     data = new JsonResult(outputresponse);
            // }
            // else
            // {
            //     outputresponse = "Copy Paste failed ";
            //     data = new JsonResult(outputresponse);
            // }

            //return rundownid;
            return data;
        }

        [HttpGet("api/getrundownlistdetails")]
        public JsonResult getrundownlistdetails(int rundownid)
        {
            dynamic data = null;
            object outputresponse = null;
            DataTable dt = null;
            string cs = _configuration.GetConnectionString("SqlServer1");

            string query = "select id" + "," + "rundownId" + "," + "status" + "," + "slugName" + "," + "slugType" + "," + "reporter" + "," + "assignTo" + "," +
            "acceptedBy" + "," + "convert(varchar, createDate, 23) as createDate" + "," + "gfx_attachment" + "," + "video_attachment" + "," + "remarks" + "," + "Story_Editor" + "," + "roleid from RundownlistDetails where rundownid=" + rundownid + "";
            string orderby = "ORDER BY id ASC";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query + orderby, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    outputresponse = dt;

                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Data is not found";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;
        }

        [HttpDelete("api/deleterundownlistdetails")]
        public JsonResult deleterundownlistdetails(int id, int rundownId)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");
            int Results = 0;
            object outputresponse = null;
            dynamic data = null;

            string query = "delete from rundownlistdetails where id=" + id + " and rundownid=" + rundownId + "";


            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Record deleted successfully";
                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Record not deleted";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;
        }

        [HttpPut("api/updaterundownlistdetails")]
        public JsonResult updaterundownlistdetails([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");

            int Results = 0;
            object outputresponse = null;
            dynamic data = null;
            string s = "";
            string query = "update RundownlistDetails set ";

            // if (objRundownlistDetailsDTO.rundownId > 0)
            // {
            //     if (s == "")
            //         s = " rundownId='" + objRundownlistDetailsDTO.rundownId + "'";
            //     else s += ",rundownId='" + objRundownlistDetailsDTO.rundownId + "'";
            // }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.status))
            {
                if (s == "")
                    s = " status='" + objRundownlistDetailsDTO.status + "'";
                else s += ",status='" + objRundownlistDetailsDTO.status + "'"; ;
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugName))
            {
                if (s == "")
                    s = "slugName='" + objRundownlistDetailsDTO.slugName + "'";
                else s += ",slugName='" + objRundownlistDetailsDTO.slugName + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.slugType))
            {
                if (s == "")
                    s = " slugType='" + objRundownlistDetailsDTO.slugType + "'";
                else s += ",slugType='" + objRundownlistDetailsDTO.slugType + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.reporter))
            {
                if (s == "")
                    s = " reporter='" + objRundownlistDetailsDTO.reporter + "'";
                else s += ", reporter='" + objRundownlistDetailsDTO.reporter + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.assignTo))
            {
                if (s == "")
                    s = "assignTo='" + objRundownlistDetailsDTO.assignTo + "'";
                else s += ", assignTo='" + objRundownlistDetailsDTO.assignTo + "'";
            }


            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.acceptedBy))
            {

                if (s == "")
                    s = " acceptedBy='" + objRundownlistDetailsDTO.acceptedBy + "'";
                else s += ",  acceptedBy='" + objRundownlistDetailsDTO.acceptedBy + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.createDate))
            {

                if (s == "")
                    s = " createDate='" + objRundownlistDetailsDTO.createDate + "'";
                else s += " ,createDate='" + objRundownlistDetailsDTO.createDate + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.gfx_attachment))
            {
                if (s == "")
                    s = " gfx_attachment='" + objRundownlistDetailsDTO.gfx_attachment + "'";
                else s += ", gfx_attachment='" + objRundownlistDetailsDTO.gfx_attachment + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.video_attachment))
            {
                if (s == "")
                    s = " video_attachment='" + objRundownlistDetailsDTO.video_attachment + "'";
                else s += ", video_attachment='" + objRundownlistDetailsDTO.video_attachment + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.story_editor))
            {
                if (s == "")
                    s = " story_editor='" + objRundownlistDetailsDTO.story_editor + "'";
                else s += ", story_editor='" + objRundownlistDetailsDTO.story_editor + "'";
            }
            if (!string.IsNullOrEmpty(objRundownlistDetailsDTO.remarks))
            {
                if (s == "")
                    s = " remarks='" + objRundownlistDetailsDTO.remarks + "'";
                else s += ", remarks='" + objRundownlistDetailsDTO.remarks + "'";
            }
            if (objRundownlistDetailsDTO.roleid>0)
            {
                if (s == "")
                    s = " roleid='" + objRundownlistDetailsDTO.roleid + "'";
                else s += ", roleid='" + objRundownlistDetailsDTO.roleid + "'";
            }
            try
            {

                if (s != "")
                {
                    query += s + " where id='" + objRundownlistDetailsDTO.id + "'and rundownId= " + objRundownlistDetailsDTO.rundownId + "";

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            Results = cmd.ExecuteNonQuery();
                        }
                    }
                    if (Results != 0)
                    {
                        outputresponse = "Record updated successfully";
                        data = new JsonResult(outputresponse);
                    }
                    else
                    {
                        outputresponse = "Record not updated";
                        data = new JsonResult(outputresponse);
                    }

                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;



        }
        [HttpPut("api/insertrundownlistdetails")]
        public JsonResult insertrundownlistdetails([FromBody] RundownlistDetail objRundownlistDetailsDTO)
        {
            string cs = _configuration.GetConnectionString("SqlServer1");

            int Results = 0;
            object outputresponse = null;
            dynamic data = null;

            string query = "insert into RundownlistDetails values ((SELECT MAX(id)+1 FROM RundownlistDetails)," + objRundownlistDetailsDTO.rundownId + ",'" + objRundownlistDetailsDTO.status +
            "','" + objRundownlistDetailsDTO.slugName + "','" + objRundownlistDetailsDTO.slugType + "','" + objRundownlistDetailsDTO.reporter +
            "','" + objRundownlistDetailsDTO.assignTo + "','" + objRundownlistDetailsDTO.acceptedBy + "','" + objRundownlistDetailsDTO.createDate +
            "','" + objRundownlistDetailsDTO.gfx_attachment + "','" + objRundownlistDetailsDTO.video_attachment + "','" + objRundownlistDetailsDTO.remarks + "','" + objRundownlistDetailsDTO.story_editor+"'," +objRundownlistDetailsDTO.roleid+ ")" + "declare @id int set @id=0 update RundownlistDetails set id=@id,@id=@id+1";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        Results = cmd.ExecuteNonQuery();
                    }
                }

                if (Results != 0)
                {
                    outputresponse = "Record inserted successfully";
                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Record not inserted";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);
            }
            return data;

        }

        [HttpGet("api/permissiondetails")]
        public JsonResult permissiondetails(int userId)
        {
            dynamic data = null;
            object outputresponse = null;
            DataTable dt = null;
            string cs = _configuration.GetConnectionString("SqlServer1");

            string query = "SELECT mp.userId, nm.roleId,nm.roleType,nm.level,nm.finalapproval FROM newsflowmaster as nm left join mapping as mp ON nm.roleId=mp.roleId where mp.userId=" + userId + "";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    outputresponse = dt;

                    data = new JsonResult(outputresponse);
                }
                else
                {
                    outputresponse = "Data is not found";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);

            }
            return data;
        }
        
        [HttpGet("api/assignto")]
        public JsonResult assignto(int userId)
        {
            dynamic data = null;
            object outputresponse = null;
            DataTable dt = null;
            int roleid = 0;
            string assignto=string.Empty;
            string cs = _configuration.GetConnectionString("SqlServer1");

            string query = "select roleid from mapping where userId=" + userId + "";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        roleid = Convert.ToInt32(cmd.ExecuteScalar());

                    }
                }
            string query1="SELECT u.firstname as assignto , mp.roleId as roleid FROM users as u INNER JOIN mapping as mp ON u.id =mp.userId where mp.roleid<"+roleid+"";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                    }

                   
                    Hashtable s = new Hashtable();                   

                    foreach(DataRow row in dt.Rows)
                    {
                        assignto += row["assignto"]+",";
                    }  
                    assignto=assignto.Remove(assignto.Length-1);    
                                 
                    s.Add("assignTo",assignto);                
                                      
                    data = new JsonResult(dt);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    //outputresponse = dt;
                    //data = new JsonResult(data );
                }
                else
                {
                    outputresponse = "Data is not found";
                    data = new JsonResult(outputresponse);
                }
            }
            catch (Exception ex)
            {
                string[] errormsg = ex.StackTrace.Split(':');
                string msg = errormsg[2];
                WriterLog.WriteErrorLog("Exception :", msg + " : " + ex.Message);
                data = new JsonResult(ex.Message);

            }
            return data;
        }
        
    }
    

}
