using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupApp.Models
{
    public class MeetupGroup
    {
        public ObservableCollection<Result> results { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string next { get; set; }
        public string method { get; set; }
        public int total_count { get; set; }
        public string link { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string lon { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public long updated { get; set; }
        public string lat { get; set; }
    }

    public class Result : IEquatable<Result>
    {
        public int utc_offset { get; set; }
        public string country { get; set; }
        public string visibility { get; set; }
        public string city { get; set; }
        public string timezone { get; set; }
        public long created { get; set; }
        public Topic[] topics { get; set; }
        public string link { get; set; }
        public float rating { get; set; }
        public string description { get; set; }
        public float lon { get; set; }
        public Group_Photo group_photo { get; set; }
        public string join_mode { get; set; }
        public Organizer organizer { get; set; }
        public int members { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string urlname { get; set; }
        public Category category { get; set; }
        public float lat { get; set; }
        public string who { get; set; }

        public bool Equals(Result other)
        {
            return this.id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }

    public class Group_Photo
    {
        public string highres_link { get; set; }
        public int photo_id { get; set; }
        public string base_url { get; set; }
        public string type { get; set; }
        public string photo_link { get; set; }
        public string thumb_link { get; set; }
    }

    public class Organizer
    {
        public int member_id { get; set; }
        public string name { get; set; }
        public Photo photo { get; set; }
    }

    public class Photo
    {
        public string highres_link { get; set; }
        public int photo_id { get; set; }
        public string base_url { get; set; }
        public string type { get; set; }
        public string photo_link { get; set; }
        public string thumb_link { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public int id { get; set; }
        public string shortname { get; set; }
    }

    public class Topic
    {
        public string urlkey { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    //public class Meta
    //{
    //    public int count { get; set; }
    //    public string description { get; set; }
    //    public string id { get; set; }
    //    public double lat { get; set; }
    //    public string link { get; set; }
    //    public double lon { get; set; }
    //    public string method { get; set; }
    //    public string next { get; set; }
    //    public string title { get; set; }
    //    public int total_count { get; set; }
    //    public long updated { get; set; }
    //    public string url { get; set; }
    //}

    //public class Category
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string shortname { get; set; }
    //}

    //public class GroupPhoto
    //{
    //    public string highres_link { get; set; }
    //    public int photo_id { get; set; }
    //    public string photo_link { get; set; }
    //    public string thumb_link { get; set; }
    //}

    //public class Organizer
    //{
    //    public int member_id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Topic
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string urlkey { get; set; }
    //}

    //public class Result : IEquatable<Result>
    //{
    //    public Category category { get; set; }
    //    public string city { get; set; }
    //    public string country { get; set; }
    //    public object created { get; set; }
    //    public string description { get; set; }
    //    public GroupPhoto group_photo { get; set; }
    //    public int id { get; set; }
    //    public string join_mode { get; set; }
    //    public double lat { get; set; }
    //    public string link { get; set; }
    //    public double lon { get; set; }
    //    public int members { get; set; }
    //    public string name { get; set; }
    //    public Organizer organizer { get; set; }
    //    public double rating { get; set; }
    //    public string state { get; set; }
    //    public List<Topic> topics { get; set; }
    //    public string urlname { get; set; }
    //    public string visibility { get; set; }
    //    public string who { get; set; }

    //    public bool Equals(Result other)
    //    {
    //        return this.id == other.id;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return id;
    //    }
    //}

    //public class MeetupGroup
    //{
    //    public Meta meta { get; set; }
    //    public ObservableCollection<Result> results { get; set; }
    //}
}
