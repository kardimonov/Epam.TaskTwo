var signal = (function () {
    return function (options) {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('/notify')
            .build();

        connection.on(options.methodName, results => {
            const searchOptions = options.searchOptions;
            if (searchOptions && typeof searchOptions.isInSearch === "function" && searchOptions.isInSearch()) {
                const updatedArea = 'updated-list';
                updatedArea.show();
                updatedArea.find('.updated-list-link').click(
                    function () {
                        $(this).off('click');
                        updatedArea.hide();
                        searchOptions.clearSearch();
                        options.listUpdater.performUpdates(results);
                    });
            } else {
                options.listUpdater.performUpdates(results);
            }
        });

        return {
            startListening: function () {
                connection.start().catch(err => console.error(err.toString()));
            }
        }
    };
}());