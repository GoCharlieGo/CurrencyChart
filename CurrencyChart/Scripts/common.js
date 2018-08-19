$(document).ready(function() {
    $('.ajax-button').on('click',
        function () {
            $('.transaction-info').css('visibility', 'visible');
            $.ajax({
                url: '/home/Ajax',
                type: 'POST',
                success: function (d) {
                    var options = {
                        title: {
                            text: "График"
                        },
                        data: [
                            {
                                type: "column",
                                dataPoints: d
                        }
                        ]
                    };
                    $('.chart-container').CanvasJSChart(options).css('height','500px');
                },
                complete: function() {
                    $('.transaction-info').removeClass('hidden');
                }
            });
        });
});