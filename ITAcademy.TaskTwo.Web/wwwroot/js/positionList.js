let positionList = (function () {
    return function (options) {
        return {
            performUpdates: function (positions) {
                let tbody = $('<tbody id="tableBody"></tbody>');
                $.each(positions, (index, position) => {
                    tbody.append('<tr>' +
                        '<td>' + position.name + '</td>' +
                        '<td>' + position.maxNumber + '</td>' +
                        '<td>' + options.links.replace(/_id_/g, position.id) +
                        '</td></tr>');
                });
                $('#tableBody').replaceWith(tbody);
                $('#tableBody') = tbody;
            }
        }
    };
}());