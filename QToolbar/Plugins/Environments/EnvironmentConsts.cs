﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public static class EnvironmentConsts
   {

      public const string QC_CHECKOUT_PATH = "QC_CHECKOUT_PATH";
      public const string PROTEUS_CHECKOUT_PATH = "PROTEUS_CHECKOUT_PATH";

      // qbc_admin.cf
      public const string QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER = "QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER";
      public const string QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME = "QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME";
      public const string QBC_ADMIN_CF_APPLICATION_WS_URL = "QBC_ADMIN_CF_APPLICATION_WS_URL";
      public const string QBC_ADMIN_CF_TOOLKIT_WS_URL = "QBC_ADMIN_CF_TOOLKIT_WS_URL";
      public const string QBC_ADMIN_CF_FILE = "QBC_ADMIN_CF_FILE";
      public const string QBC_ADMIN_CF_REMOTE = "QBC_ADMIN_CF_REMOTE";
      public const string QBC_ADMIN_CF_ENVIRONMENT_KEY = "QBC_ADMIN_CF_ENVIRONMENT_KEY";

      // web services ports
      public const string QCS_APP_WS_URL_PORT = "QCS_APP_WS_URL_PORT";
      public const string LEGAL_APP_WS_URL_PORT = "LEGAL_APP_WS_URL_PORT";

      // QBCOLLECTION PLUS db
      public const string QBCOLLECTION_PLUS_MAJOR_VERSION = "QBCOLLECTION_PLUS_MAJOR_VERSION";
      public const string QBCOLLECTION_PLUS_MINOR_VERSION = "QBCOLLECTION_PLUS_MINOR_VERSION";

      // SYSTEM PARAMS
      public const string AT_SYSTEM_PARAMS_DIALER_DB_NAME = "AT_SYSTEM_PARAMS_DIALER_DB_NAME";
      public const string AT_SYSTEM_PARAMS_QBC_NAME = "AT_SYSTEM_PARAMS_QBC_NAME";
      public const string AT_SYSTEM_PARAMS_QBC_SERVER = "AT_SYSTEM_PARAMS_QBC_SERVER";
      public const string AT_SYSTEM_PARAMS_FILE_SERVER_NAME = "AT_SYSTEM_PARAMS_FILE_SERVER_NAME";
      public const string AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME = "AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME";
      public const string AT_SYSTEM_PARAMS_QBA_NAME = "AT_SYSTEM_PARAMS_QBA_NAME";
      public const string AT_SYSTEM_PARAMS_QBA_SERVER = "AT_SYSTEM_PARAMS_QBA_SERVER";
      public const string AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS = "AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS";
      public const string AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE = "AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE";


      // BI GLM INSTALLATION TABLE
      public const string BI_GLM_INSTALLATION_STEM_NAME = "BI_GLM_INSTALLATION_STEM_NAME";
      public const string BI_GLM_INSTALLATION_INST_ROOT = "BI_GLM_INSTALLATION_INST_ROOT";
      public const string BI_GLM_INSTALLATION_INST_STEM_NAME = "BI_GLM_INSTALLATION_INST_STEM_NAME";
      public const string BI_GLM_INSTALLATION_INST_SERVER = "BI_GLM_INSTALLATION_INST_SERVER";
      public const string BI_GLM_INSTALLATION_INST_DB_NAME = "BI_GLM_INSTALLATION_INST_DB_NAME";
      public const string BI_GLM_INSTALLATION_INST_DBUSER = "BI_GLM_INSTALLATION_INST_DBUSER";
      public const string BI_GLM_INSTALLATION_INST_DBPASSW = "BI_GLM_INSTALLATION_INST_DBPASSW";
      public const string BI_GLM_INSTALLATION_QBA_SERVER = "BI_GLM_INSTALLATION_QBA_SERVER";
      public const string BI_GLM_INSTALLATION_QBA_DB_NAME = "BI_GLM_INSTALLATION_QBA_DB_NAME";
      public const string BI_GLM_INSTALLATION_QBA_DBUSER = "BI_GLM_INSTALLATION_QBA_DBUSER";
      public const string BI_GLM_INSTALLATION_QBA_DBPASSW = "BI_GLM_INSTALLATION_QBA_DBPASSW";
      public const string BI_GLM_INSTALLATION_QD3F_SERVER = "BI_GLM_INSTALLATION_QD3F_SERVER";
      public const string BI_GLM_INSTALLATION_QD3F_DB_NAME = "BI_GLM_INSTALLATION_QD3F_DB_NAME";
      public const string BI_GLM_INSTALLATION_QD3F_DBUSER = "BI_GLM_INSTALLATION_QD3F_DBUSER";
      public const string BI_GLM_INSTALLATION_QD3F_DBPASSW = "BI_GLM_INSTALLATION_QD3F_DBPASSW";

      // AT_SYSTEM_PREF
      public const string AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY = "AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY";
      public const string AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY = "AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY";
      public const string AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH = "AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH";
      public const string AT_SYSTEM_PREF_WORDTEMPLATESFOLDER = "AT_SYSTEM_PREF_WORDTEMPLATESFOLDER";
      public const string AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL = "AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL";
      public const string AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL = "AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL";

      // PARAMETER SETS NAMES
      public const string PARAM_SET_EQ_COLLECTION_PLUS_SERVER = "EQ:QBCollectionPlus Server";
      public const string PARAM_SET_EQ_COLLECTION_PLUS_DBNAME = "EQ:QBCollectionPlus Database";

      public const string PARAM_SET_EQ_ANALYTICS_SERVER = "EQ:Analytics Server";
      public const string PARAM_SET_EQ_ANALYTICS_DBNAME = "EQ:Analytics Database";

      public const string PARAM_SET_CONNECTION_QBC_ADMIN_CF_QBCOLLECTION_PLUS = "qbc_admin:Connection to QBCollectionPlus";
      public const string PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_DIALER = "AT_SYSTEM_PARAMS:Connection to Dialer db";
      public const string PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_QBCOLLECTION_PLUS = "AT_SYSTEM_PARAMS:Connection to QBCollectionPlus";
      public const string PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_ANALYTICS = "AT_SYSTEM_PARAMS:Connection to Analytics";
      public const string PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_ANALYTICS2 = "AT_SYSTEM_PARAMS:Connection2 to Analytics";

      public const string PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_QBCOLLECTION_PLUS = "BI_GLM_INSTALLATION:Connection to QBCollectionPlus";
      public const string PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_ANALYTICS = "BI_GLM_INSTALLATION:Connection to Analytics";
      public const string PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_D3F = "BI_GLM_INSTALLATION:Connection to D3F";


   }
}
