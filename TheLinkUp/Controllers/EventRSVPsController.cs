using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TheLinkUp.Models;
using Utilities;

namespace TheLinkUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRSVPsController : ControllerBase
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        [HttpGet]
        [Route("GetByEvent")]
        public List<RSVP> GetByEvent(string eventID)
        {
            List<RSVP> rsvpList = new List<RSVP>();

            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "SELECT RSVPId, MemberID, EventID, EventName, EventDate, VenueName, City, State FROM EventRSVPs WHERE EventID = @EventID";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@EventID", eventID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                RSVP rsvp = new RSVP();

                rsvp.RSVPId = int.Parse(record["RSVPId"].ToString());
                rsvp.MemberID = int.Parse(record["MemberID"].ToString());
                rsvp.EventID = record["EventID"].ToString();
                rsvp.EventName = record["EventName"].ToString();
                rsvp.EventDate = record["EventDate"].ToString();
                rsvp.VenueName = record["VenueName"].ToString();
                rsvp.City = record["City"].ToString();
                rsvp.State = record["State"].ToString();

                rsvpList.Add(rsvp);
            }

            return rsvpList;
        }

        [HttpPost]
        [Route("Add")]
        public bool Add([FromBody] RSVP rsvp)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddEventRSVP";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@MemberID", rsvp.MemberID);
            objCommand.Parameters.AddWithValue("@EventID", rsvp.EventID);
            objCommand.Parameters.AddWithValue("@EventName", rsvp.EventName);
            objCommand.Parameters.AddWithValue("@EventDate", rsvp.EventDate);
            objCommand.Parameters.AddWithValue("@VenueName", rsvp.VenueName);
            objCommand.Parameters.AddWithValue("@City", rsvp.City);
            objCommand.Parameters.AddWithValue("@State", rsvp.State);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        [Route("Update")]
        public bool Update([FromBody] RSVP rsvp)
        {
            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "UPDATE EventRSVPs SET EventName = @EventName, EventDate = @EventDate, VenueName = @VenueName, City = @City, State = @State WHERE RSVPId = @RSVPId";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@RSVPId", rsvp.RSVPId);
            objCommand.Parameters.AddWithValue("@EventName", rsvp.EventName);
            objCommand.Parameters.AddWithValue("@EventDate", rsvp.EventDate);
            objCommand.Parameters.AddWithValue("@VenueName", rsvp.VenueName);
            objCommand.Parameters.AddWithValue("@City", rsvp.City);
            objCommand.Parameters.AddWithValue("@State", rsvp.State);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public bool Delete(int rsvpId)
        {
            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "DELETE FROM EventRSVPs WHERE RSVPId = @RSVPId";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@RSVPId", rsvpId);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}