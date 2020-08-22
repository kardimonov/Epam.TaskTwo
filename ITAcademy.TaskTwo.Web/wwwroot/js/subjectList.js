let subjectList = (function () {    
    return function (options) {
        return {
            performUpdates: function (subjects) {
                let tbody = $('<tbody id="tableBody"></tbody>');
                $.each(subjects, (index, subject) => {
                    tbody.append('<tr>' +
                        '<td>' + subject.name + '</td>' +
                        '<td>' + options.links.replace(/_id_/g, subject.id) +
                        '</td></tr>');
                });
                $('#tableBody').replaceWith(tbody);
                $('#tableBody') = tbody;
            }
        }
    };
}());