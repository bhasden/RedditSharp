﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditSharp.Things
{
    public class ModAction : Thing
    {
        public ModAction(Reddit reddit, JToken json) : base(reddit, json) {
        }

        /// <summary>
        /// Type of action.
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(ModActionTypeConverter))]
        public ModActionType Action { get; }

        /// <summary>
        /// DateTime of the action.
        /// </summary>
        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime? TimeStamp { get; }

        /// <summary>
        /// Populated when <see cref="Action"/> is WikiBan, BanUser, or UnBanUser.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; }

        /// <summary>
        /// Action details.
        /// </summary>
        [JsonProperty("details")]
        public string Details { get; }

        /// <summary>
        /// Base 36 id of the moderator who performed the action.
        /// </summary>
        [JsonProperty("mod_id36")]
        public string ModeratorId { get; }

        /// <summary>
        /// Name of moderator who performed the action.
        /// </summary>
        [JsonProperty("mod")]
        public string ModeratorName { get; }

        /// <summary>
        /// Target author name of the item against which this moderation action was performed.
        /// </summary>
        [JsonProperty("target_author")]
        public string TargetAuthorName { get; }

        /// <summary>
        /// Target full name of the item against which this moderation action was performed.
        /// </summary>
        [JsonProperty("target_fullname")]
        public string TargetThingFullname { get; }

        /// <summary>
        /// Permalink of the item against which this moderation action was performed.
        /// </summary>
        [JsonProperty("target_permalink")]
        public string TargetThingPermalink { get; }

        /// <summary>
        /// Base 36 id of the subreddit.
        /// </summary>
        [JsonProperty("sr_id36")]
        public string SubredditId { get; }

        /// <summary>
        /// Subreddit name.
        /// </summary>
        [JsonProperty("subreddit")]
        public string SubredditName { get; }

        /// <summary>
        /// Populated when target is a comment.
        /// </summary>
        [JsonProperty("target_body")]
        public string TargetBody { get; }

        /// <summary>
        /// Populated when target is a post.
        /// </summary>
        [JsonProperty("target_title")]
        public string TargetTitle { get; }

        /// <summary>
        /// Author of the item against which this moderation action was performed.
        /// </summary>
        [JsonIgnore]
        //TODO discuss
        public RedditUser TargetAuthor =>
          Task.Run(async () => {
              return await Reddit.GetUserAsync(TargetAuthorName).ConfigureAwait(false);
            }).Result;

        /// <summary>
        /// Item against which this moderation action was performed.
        /// </summary>
        [JsonIgnore]
        //TODO discuss
        public Thing TargetThing =>
          Task.Run(async () => {
              return await Reddit.GetThingByFullnameAsync(TargetThingFullname).ConfigureAwait(false);
            }).Result ;

        protected override JToken GetJsonData(JToken json) => json["data"];
   }
}
