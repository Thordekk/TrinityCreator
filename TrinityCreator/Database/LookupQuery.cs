﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TrinityCreator.Database
{
    internal class LookupQuery : Connection
    {
        internal static DataTable FindItemsByName(string partialName)
        {
            return ExecuteQuery(
                "SELECT entry, displayid, name FROM item_template WHERE name LIKE '%" + MySqlHelper.EscapeString(partialName) + "%' ORDER BY entry DESC LIMIT 200;");
        }

        internal static DataTable FindQuestByName(string partialName)
        {
            return ExecuteQuery(
                "SELECT Id, LogTitle FROM quest_template WHERE LogTitle LIKE '%" + MySqlHelper.EscapeString(partialName) + "%' ORDER BY Id DESC LIMIT 200;");
        }

        internal static DataTable FindCreatureByName(string partialName)
        {
            return ExecuteQuery(
                "SELECT entry, modelid1, name FROM creature_template WHERE name LIKE '%" + MySqlHelper.EscapeString(partialName) + "%' ORDER BY entry DESC LIMIT 200;");
        }

        internal static DataTable FindGoByName(string partialName)
        {
            return ExecuteQuery(
                "SELECT entry, displayId, name FROM gameobject_template WHERE name LIKE '%" + MySqlHelper.EscapeString(partialName) + "%' ORDER BY entry DESC LIMIT 200;");
        }

        internal static DataTable GetSpells(string partialName)
        {
            return ExecuteQuery(
                "SELECT Id, Comment FROM spell_dbc WHERE Comment LIKE '%" + MySqlHelper.EscapeString(partialName) + "%' ORDER BY Id DESC LIMIT 200;");
        }

        /// <summary>
        /// Returns next ID for the selected table
        /// 
        /// </summary>
        /// <param name="table">table to check</param>
        /// <param name="primaryKey">usually id or entry</param>
        /// <returns></returns>
        internal static int GetNextId(string table, string primaryKey)
        {
            object result = ExecuteScalar(string.Format("SELECT MAX({0}) FROM {1};", primaryKey, table), requestConfig:false);
            if (result == null || result is DBNull)
                return 0;
            else return Convert.ToInt32(result) + 1;
        }

        internal static int GetCreatureIdFromDisplayId(int displayId)
        {
            object result = ExecuteScalar(string.Format(
                "SELECT entry FROM creature_template WHERE modelid1 = {0} OR modelid2 = {0} OR modelid3 = {0} OR modelid4 = {0};", displayId));
            if (result == null)
                return 0;
            else return Convert.ToInt32(result);
        }

        internal static int GetItemDisplayFromEntry(int entry)
        {
            object result = ExecuteScalar(string.Format(
                "SELECT displayid FROM item_template WHERE entry = {0};", entry));
            if (result == null)
                return 0;
            else return Convert.ToInt32(result);
        }
    }
}