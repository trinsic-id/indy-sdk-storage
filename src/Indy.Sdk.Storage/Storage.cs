using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Streetcred.Indy.Sdk
{
    public static class Storage
    {
        /// <summary>
        /// Wallet storage registrations by name.
        /// </summary>
        private static readonly ConcurrentBag<WalletStorage> RegisteredWalletStores =
            new ConcurrentBag<WalletStorage>();

        /// <summary>
        /// Register custom wallet storage implementation.
        /// </summary>
        /// <param name="storageType">Storage type name.</param>
        /// <param name="storage">Storage implementation instance</param>
        /// <returns></returns>
        public static Task RegisterWalletStorageAsync(string storageType, IWalletStorage storage)
        {
            if (string.IsNullOrEmpty(storageType)) throw new ArgumentNullException(nameof(storageType));
            if (storage == null) throw new ArgumentNullException(nameof(storage));

            var taskCompletionSource = new TaskCompletionSource<bool>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var walletStorage = new WalletStorage(storage);
            RegisteredWalletStores.Add(walletStorage);

            var result = NativeMethods.indy_register_wallet_storage(
                commandHandle,
                storageType,
                walletStorage.WalletCreateCallback,
                walletStorage.WalletOpenCallback,
                walletStorage.WalletCloseCallback,
                walletStorage.WalletDeleteCallback,
                walletStorage.WalletAddRecordCallback,
                walletStorage.WalletUpdateRecordValueCallback,
                walletStorage.WalletUpdateRecordTagsCallback,
                walletStorage.WalletAddRecordTagsCallback,
                walletStorage.WalletDeleteRecordTagsCallback,
                walletStorage.WalletDeleteRecordCallback,
                walletStorage.WalletGetRecordCallback,
                walletStorage.WalletGetRecordIdCallback,
                walletStorage.WalletGetRecordTypeCallback,
                walletStorage.WalletGetRecordValueCallback,
                walletStorage.WalletGetRecordTagsCallback,
                walletStorage.WalletFreeRecordCallback,
                walletStorage.WalletGetStorageMetadataCallback,
                walletStorage.WalletSetStorageMetadataCallback,
                walletStorage.WalletFreeStorageMetadataCallback,
                walletStorage.WalletSearchRecordsCallback,
                walletStorage.WalletSearchAllRecordsCallback,
                walletStorage.WalletGetSearchTotalCountCallback,
                walletStorage.WalletFetchSearchNextRecordCallback,
                walletStorage.WalletFreeSearchCallback,
                Utils.TaskCompletingNoValueCallback);

            Utils.CheckResult(result);

            return taskCompletionSource.Task;
        }
    }
}
