let searchEngine = (function () {
    return function (options) {
        let isInSearch = false;
        const $field = $('#searchString');
        let previousSearch = '';
        $field.on('input', function () {
            const lookFor = $.trim($field.val());
            const toExecute = function () {
                if (previousSearch === lookFor) return;
                previousSearch = lookFor;
                isInSearch = lookFor !== '';
                $.ajax({
                    method: 'POST',
                    url: options.searchUrl,
                    data: {
                        searchString: lookFor
                    }                    
                }).done(function (response) {
                    if (response.searchString === lookFor || response.searchString == null && lookFor === '') {
                        options.listUpdater.performUpdates(response.model);
                    }
                });
            }

            if (lookFor.length > 0 && lookFor.length <= 2) {
                setTimeout(function () {
                    if (lookFor === $.trim($field.val())) {
                        toExecute();
                    }
                },
                    2000); 
            } else {
                toExecute();
            }
        });
        return {
            isInSearch: function () {
                return isInSearch;
            },
            clearSearch: function () {
                $field.val('');
            }
        }
    };
}());