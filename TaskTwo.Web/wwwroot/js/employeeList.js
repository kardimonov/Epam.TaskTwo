let employeeList = (function () {
    function formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString();
    }

    function insertPhones(phones) {
        if (!phones) return '';
        let result = '';
        $.each(phones,
            (index, phone) => {
                result += '<div>' + phone + '</div>';
            });
        return result;
    }

    return function (options) {
        return {
            performUpdates: function (employees) {
                let tbody = $('<tbody id="tableBody"></tbody>');
                $.each(employees, (index, employee) => {
                    tbody.append('<tr>' +
                        '<td>' + employee.surName + ' ' + employee.firstName +
                        ' ' + (employee.secondName || '') + '</td>' +
                        '<td>' + formatDate(employee.birth) + '</td>' +
                        '<td>' + (employee.email || '') + '</td>' +                        
                        '<td x-ms-format-detection="none">' + insertPhones(employee.phones) + '</td>' +                                               
                        '<td>' + options.links.replace(/_id_/g, employee.id) +
                        '</td></tr>');
                });
                $('#tableBody').replaceWith(tbody);
                $('#tableBody') = tbody;
            }
        }
    };
}());