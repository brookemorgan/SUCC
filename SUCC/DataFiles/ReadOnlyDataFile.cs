﻿using System;

namespace SUCC
{
    /// <summary>
    /// A read-only version of DataFile. Data can be read from disk, but not saved to disk.
    /// </summary>
    public class ReadOnlyDataFile : DataFileBase
    {
        /// <summary>
        /// Creates a new ReadOnlyDataFile object corresponding to a SUCC file in system storage.
        /// </summary>
        /// <param name="path"> the path of the file. Can be either absolute or relative to the default path. </param>
        /// <param name="defaultFileText"> optionally, if there isn't a file at the path, one can be created from the text supplied here. </param>
        /// <param name="autoReload"> if true, the DataFile will automatically reload when the file changes on disk. </param>
        public ReadOnlyDataFile(string path, string defaultFileText = null, bool autoReload = false) : base(path, defaultFileText, autoReload) { }

        /// <summary> Get some data from the file, or return a default value if the data does not exist </summary>
        /// <param name="key"> what the data is labeled as within the file </param>
        /// <param name="defaultValue"> if the key does not exist in the file, this value is returned instead </param>
        public override T Get<T>(string key, T defaultValue = default) => base.Get(key, defaultValue);

        /// <summary> Non-generic version of Get. You probably want to use Get. </summary>
        /// <param name="type"/> the type to get the data as </param>
        /// <param name="key"> what the data is labeled as within the file </param>
        /// <param name="DefaultValue"> if the key does not exist in the file, this value is returned instead </param>
        public override object GetNonGeneric(Type type, string key, object defaultValue)
        {
            if (!KeyExists(key)) return defaultValue;

            var node = TopLevelNodes[key];
            return NodeManager.GetNodeData(node, type);
        }
    }
}