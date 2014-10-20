﻿/*
 * Copyright (c) Contributors, http://whitecore-sim.org/, http://aurora-sim.org
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using WhiteCore.Framework.Utilities;

namespace WhiteCore.DataManager.Migration.Migrators.Groups
{
    /// <summary>
    ///     Changes:
    ///
    /// 	* Adding the 2 tables for the Proposals Tab for older viewers
    /// </summary>
    public class GroupsMigrator_5 : Migrator
    {
        public GroupsMigrator_5()
        {
            Version = new Version(0, 0, 5);
            MigrationName = "Groups";
            
            schema = new List<SchemaDefinition>();
            
            AddSchema("group_proposals", ColDefs(
            	ColDef("GroupID", ColumnTypes.String50),
            	ColDef("Duration", ColumnTypes.Integer11),
            	ColDef("Majority", ColumnTypes.Float),
            	ColDef("Text", ColumnTypes.Text),
            	ColDef("Quorum", ColumnTypes.Integer11),
            	ColDef("Session", ColumnTypes.String50),
            	ColDef("BallotInitiator", ColumnTypes.String50),
            	ColDef("Created", ColumnTypes.DateTime),
            	ColDef("Ending", ColumnTypes.DateTime),
            	ColDef("VoteID", ColumnTypes.String50)));
            
            AddSchema("group_proposals_votes", ColDefs(
            	ColDef("ProposalID", ColumnTypes.String50),
            	ColDef("UserID", ColumnTypes.String50),
            	ColDef("Vote", ColumnTypes.String10)));
        }

        protected override void DoCreateDefaults(IDataConnector genericData)
        {
            EnsureAllTablesInSchemaExist(genericData);
        }

        protected override bool DoValidate(IDataConnector genericData)
        {
            return TestThatAllTablesValidate(genericData);
        }

        protected override void DoMigrate(IDataConnector genericData)
        {
            DoCreateDefaults(genericData);
        }

        protected override void DoPrepareRestorePoint(IDataConnector genericData)
        {
            CopyAllTablesToTempVersions(genericData);
        }
    }
}