$(function () {
    var current = "#login";
    $('.nav-tabs li a').each(function () {
        var $this = $(this);
        if (current.indexOf($this.attr('href')) !== -1) {
            $this.addClass('active');
        }
    })
})