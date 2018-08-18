$(document).ready(function() {
    $('.razor-button').on('click',
        function() {
            alert('Razor');
            $.ajax({
                url:'/home/RazorPartial',
                type: 'POST',
                success: function (data) {
                    $('.chart-container').html(data);
                }
            });
        });
    $('.ajax-button').on('click',
        function() {
            alert('Ajax');
        });
});