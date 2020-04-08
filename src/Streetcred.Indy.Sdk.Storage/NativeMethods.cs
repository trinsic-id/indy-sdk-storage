using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Hyperledger.Indy;
using static Streetcred.Indy.Sdk.Utils;

namespace Streetcred.Indy.Sdk
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class NativeMethods
    {
        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false,
            ThrowOnUnmappableChar = true)]
        internal static extern int indy_register_wallet_storage(int command_handle, string type_,
            WalletCreateDelegate create,
            WalletOpenDelegate open,
            WalletCloseDelegate close,
            WalletDeleteDelegate delete,
            WalletAddRecordDelegate add_record,
            WalletUpdateRecordValueDelegate update_record_value,
            WalletUpdateRecordTagsDelegate update_record_tags,
            WalletAddRecordTagsDelegate add_record_tags,
            WalletDeleteRecordTagsDelegate delete_record_tags,
            WalletDeleteRecordDelegate delete_record,
            WalletGetRecordDelegate get_record,
            WalletGetRecordIdDelegate get_record_id,
            WalletGetRecordTypeDelegate get_record_type,
            WalletGetRecordValueDelegate get_record_value,
            WalletGetRecordTagsDelegate get_record_tags,
            WalletFreeRecordDelegate free_record,
            WalletGetStorageMetadataDelegate get_storage_metadata,
            WalletSetStorageMetadataDelegate set_storage_metadata,
            WalletFreeStorageMetadataDelegate free_storage_metadata,
            WalletSearchRecordsDelegate search_records,
            WalletSearchAllRecordsDelegate search_all_records,
            WalletGetSearchTotalCountDelegate get_search_total_count,
            WalletFetchSearchNextRecordDelegate fetch_search_next_record,
            WalletFreeSearchDelegate free_search,
            IndyMethodCompletedDelegate cb);

        internal delegate ErrorCode WalletCreateDelegate(string name, string config, string credentials_json,
            string metadata);

        internal delegate ErrorCode WalletOpenDelegate(string name, string config, string credentials_json,
            ref int storage_handle_p);

        internal delegate ErrorCode WalletCloseDelegate(int storage_handle);

        internal delegate ErrorCode WalletDeleteDelegate(string name, string config, string credentials_json);

        internal delegate ErrorCode WalletAddRecordDelegate(int storage_handle, string type_, string id, IntPtr value,
            int value_len, string tags_json);

        internal delegate ErrorCode WalletUpdateRecordValueDelegate(int storage_handle, string type_, string id,
            IntPtr value, int value_len);

        internal delegate ErrorCode WalletUpdateRecordTagsDelegate(int storage_handle, string type_, string id,
            string tags_json);

        internal delegate ErrorCode WalletAddRecordTagsDelegate(int storage_handle, string type_, string id,
            string tags_json);

        internal delegate ErrorCode WalletDeleteRecordTagsDelegate(int storage_handle, string type_, string id,
            string tag_names_json);

        internal delegate ErrorCode WalletDeleteRecordDelegate(int storage_handle, string type_, string id);

        internal delegate ErrorCode WalletGetRecordDelegate(int storage_handle, string type_, string id,
            string options_json, ref int record_handle_p);

        internal delegate ErrorCode WalletGetRecordIdDelegate(int storage_handle, int record_handle,
            ref string record_id_p);

        internal delegate ErrorCode WalletGetRecordTypeDelegate(int storage_handle, int record_handle,
            ref string record_type_p);

        internal delegate ErrorCode WalletGetRecordValueDelegate(int storage_handle, int record_handle,
            ref IntPtr record_value_p, ref IntPtr record_value_len_p);

        internal delegate ErrorCode WalletGetRecordTagsDelegate(int storage_handle, int record_handle,
            ref string record_tags_p);

        internal delegate ErrorCode WalletFreeRecordDelegate(int storage_handle, int record_handle);

        internal delegate ErrorCode WalletGetStorageMetadataDelegate(int storage_handle, ref string metadata_p,
            ref int metadata_handle);

        internal delegate ErrorCode WalletSetStorageMetadataDelegate(int storage_handle, string metadata_p);

        internal delegate ErrorCode WalletFreeStorageMetadataDelegate(int stroage_handle, int metadata_handle);

        internal delegate ErrorCode WalletSearchRecordsDelegate(int storage_handle, string type_, string query_json,
            string options_json, ref int search_handle_p);

        internal delegate ErrorCode WalletSearchAllRecordsDelegate(int storage_handle, ref int search_handle_p);

        internal delegate ErrorCode WalletGetSearchTotalCountDelegate(int storage_handle, int search_handle,
            ref IntPtr total_count_p);

        internal delegate ErrorCode WalletFetchSearchNextRecordDelegate(int storage_handle, int search_handle,
            ref int record_handle_p);

        internal delegate ErrorCode WalletFreeSearchDelegate(int storage_handle, int search_handle);
    }
}