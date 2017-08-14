using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class ClassroomGateway : Gateway
    {
        public List<Classroom> GetAllRooms()
        {
            Query = "SELECT * FROM Classroom";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Classroom> allClassrooms = new List<Classroom>();


            while (Reader.Read())
            {
                Classroom classroom = new Classroom();
                classroom.Id = (int)Reader["Id"];
                classroom.RoomNo = Reader["RoomNo"].ToString();
                allClassrooms.Add(classroom);
            }

            Reader.Close();
            Connection.Close();
            return allClassrooms;
        }


        public int SaveSchedule(RoomAllocationVM roomAllocation)
        {
            Query = "INSERT INTO ClassroomAllocation Values(@courseId, @roomId, @Day, @fromTime, @toTime, @allocationStatus, @schedule)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = roomAllocation.CourseId;
            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = roomAllocation.RoomId;
            Command.Parameters.Add("Day", SqlDbType.VarChar);
            Command.Parameters["Day"].Value = roomAllocation.Day;
            Command.Parameters.Add("fromTime", SqlDbType.Time);
            Command.Parameters["fromTime"].Value = roomAllocation.FromTime.TimeOfDay;
            Command.Parameters.Add("toTime", SqlDbType.Time);
            Command.Parameters["toTime"].Value = roomAllocation.ToTime.TimeOfDay;
            Command.Parameters.Add("allocationStatus", SqlDbType.VarChar);
            Command.Parameters["allocationStatus"].Value = "Allocated";
            Command.Parameters.Add("schedule", SqlDbType.VarChar);
            Command.Parameters["schedule"].Value = roomAllocation.Schedule;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool ScheduleExist(RoomAllocationVM roomAllocation)
        {
            bool overlap = false;
            Query = "SELECT * FROM ClassroomAllocation where RoomId=@roomId and Day=@Day and @fromTime < ToTime " +
                    "and FromTime< @toTime and AllocationStatus='Allocated'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = roomAllocation.RoomId;
            Command.Parameters.Add("Day", SqlDbType.VarChar);
            Command.Parameters["Day"].Value = roomAllocation.Day;
            Command.Parameters.Add("fromTime", SqlDbType.Time);
            Command.Parameters["fromTime"].Value = roomAllocation.FromTime.TimeOfDay; ;
            Command.Parameters.Add("toTime", SqlDbType.Time);
            Command.Parameters["toTime"].Value = roomAllocation.ToTime.TimeOfDay; 
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                overlap = true;
            }

            Reader.Close();
            Connection.Close();
            return overlap;
        }

        public List<ClassSchedule> GetGetClassScheduleByDepartment(int departmentId)
        {
            Query = "SELECT * FROM Course_Schedule where deptId =@deptId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = departmentId;
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassSchedule> classSchedules = new List<ClassSchedule>();


            while (Reader.Read())
            {
                ClassSchedule schedule = new ClassSchedule();
                schedule.CourseCode = Reader["Code"].ToString();
                schedule.CourseName = Reader["Name"].ToString();
                schedule.Schedule = Reader["ScheduleInfo"].ToString();
                if (schedule.Schedule == "")
                {
                    schedule.Schedule = "Not Scheduled yet";
                }
                var update = classSchedules.Find(c => c.CourseCode == schedule.CourseCode);
                int i = classSchedules.FindIndex(c => c.CourseCode == schedule.CourseCode);
                if (update != null && update.Schedule != "Not Scheduled yet")
                {
                    classSchedules.RemoveAt(i);
                    
                    update.Schedule += ";<br/>" + schedule.Schedule;
                    classSchedules.Add(update);
                }
                else if (update != null && update.Schedule == "Not Scheduled yet")
                {
                    classSchedules.RemoveAt(i);
                    classSchedules.Add(schedule);
                }
                else
                {
                    classSchedules.Add(schedule);
                }

            }

            Reader.Close();
            Connection.Close();
            return classSchedules;
        }
        public int UnassignAllCourses()
        {
            Query = "UPDATE ClassroomAllocation SET AllocationStatus='Not Allocated Yet', ScheduleInfo=''  WHERE AllocationStatus ='Allocated'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}