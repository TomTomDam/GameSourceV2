$(document).ready(function () {
    var gameId = $('#gameId').val();

    createReviewPartialView(gameId);

    //$('#create-review-btn').on('click', function (e) {
    //    createReview(gameId);
    //});

    function createReviewPartialView(gameId) {
        $.ajax({
            url: '/game/review/' + gameId + '/create/',
            type: 'GET',
            success: function (result) {
                $('#create-review').html(result);
                $('#create-review').show();
                console.log("GET /game/review/create/ Success!")
            },
            error: function (xhr) {
                console.log("GET /game/review/create/" + " Error: " + xhr.statusText);
            }
        });
    }

    //function createReview(gameId) {
    //    var reviewObject = {
    //        "title": $('#create-review-title-input').val(),
    //        "body": $('#create-review-body-input').val(),
    //        "gameId": gameId
    //    };

    //    $.ajax({
    //        url: '/game/review/create/',
    //        data: JSON.stringify(reviewObject),
    //        type: 'POST',
    //        success: function (result) {
    //            console.log("POST /game/review/create/" + " Success!")
    //        },
    //        error: function (xhr) {
    //            console.log("POST /game/review/create/" + " Error: " + xhr.statusText);
    //        }
    //    });
    //}
});