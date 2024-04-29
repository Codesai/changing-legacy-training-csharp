require('code-forensics').configure(
    {
        repository: {
            rootPath: "repositories/[name-of-project]",
        }
    }
);

function defaultTask(cb) {
    cb();
}

exports.default = defaultTask;