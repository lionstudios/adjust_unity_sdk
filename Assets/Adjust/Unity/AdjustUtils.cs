﻿using System;
using System.Collections.Generic;

using UnityEngine;

namespace com.adjust.sdk
{
    public class AdjustUtils
    {
        #region Constants
        public static string KeyAdid = "adid";
        public static string KeyMessage = "message";
        public static string KeyNetwork = "network";
        public static string KeyAdgroup = "adgroup";
        public static string KeyCampaign = "campaign";
        public static string KeyCreative = "creative";
        public static string KeyWillRetry = "willRetry";
        public static string KeyTimestamp = "timestamp";
        public static string KeyEventToken = "eventToken";
        public static string KeyClickLabel = "clickLabel";
        public static string KeyTrackerName = "trackerName";
        public static string KeyTrackerToken = "trackerToken";
        public static string KeyJsonResponse = "jsonResponse";
        #endregion

        #region Public methods
        public static int ConvertLogLevel (AdjustLogLevel? logLevel)
        {
            if (logLevel == null)
            {
                return -1;
            }

            return (int)logLevel;
        }

        public static int ConvertBool (bool? value)
        {
            if (value == null)
            {
                return -1;
            }

            if (value.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static double ConvertDouble (double? value)
        {
            if (value == null)
            {
                return -1;
            }

            return (double)value;
        }

        public static string ConvertListToJson (List<String> list)
        {
            if (list == null)
            {
                return null;
            }

            var jsonArray = new JSONArray ();
            
            foreach (var listItem in list)
            {
                jsonArray.Add (new JSONData (listItem));
            }

            return jsonArray.ToString ();
        }

        public static void PrintJsonResponse (Dictionary<string, object> dictionary)
        {
			if (dictionary == null) {
				return;
			}

            foreach (KeyValuePair<string, object> pair in dictionary)
            {
				String valueString = pair.Value as string;

				if (valueString != null) {
					Debug.Log ("Key = " + pair.Key);
					Debug.Log ("Value = " + valueString);
					continue;
				}

				Dictionary<string, object> valueDictionary = pair.Value as Dictionary<string, object>;

				PrintJsonResponse (valueDictionary);

				/*
                Type t = pair.Value.GetType ();
                bool isDict = t.IsGenericType && t.GetGenericTypeDefinition () == typeof(Dictionary<,>);

                if (isDict == true)
                {
                    PrintJsonResponse ((Dictionary<string, object>)pair.Value);
                }
                else
                {
                    Debug.Log ("Key = " + pair.Key);
                    Debug.Log ("Value = " + pair.Value);
                }
                */
            }
        }

        public static String GetJsonString (JSONNode node, string key)
        {
			if (node == null)
			{
				return null;
			}

			// access value object and cast it to JSONData
			var nodeValue = node [key] as JSONData;

			if (nodeValue == null)
            {
                return null;
            }

			return nodeValue.Value;
        }

        public static void WriteJsonResponseDictionary (JSONClass jsonObject, Dictionary<string, object> output)
        {
            foreach (KeyValuePair<string, JSONNode> pair in jsonObject)
            {
				// try to cast value as a complex object
				var subNode = pair.Value.AsObject;
				var key = pair.Key;

				// value is not a complex object
				if (subNode == null) {
					var value = pair.Value.Value;
					output.Add (key, value);
					continue;
				}

				// create new dictionary for complex type
				var newSubDictionary = new Dictionary<string, object> ();
				// save it in the current dictionary
				output.Add (key, newSubDictionary);
				// recursive call to fill new dictionary
				WriteJsonResponseDictionary(subNode, newSubDictionary);
				/*
                if (pair.Value.AsObject == null)
                {
                    output.Add (pair.Key, pair.Value);
                }
                else
                {
                    output.Add (pair.Key, new Dictionary<string, object> ());
                    WriteJsonResponseDictionary (pair.Value.AsObject, (Dictionary<string, object>)output [pair.Key]);
                }
                */
            }
        }
        #endregion
    }
}
