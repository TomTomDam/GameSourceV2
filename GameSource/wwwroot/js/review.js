$(document).ready(function () {
    //var userId = $('#userId').val();

    createReviewPartialView();

    //$('#create-review-btn').on('click', function (e) {
    //    createReview(userId);
    //});

    function createReviewPartialView() {
        $.ajax({
            url: '/review/create/',
            type: 'GET',
            success: function (result) {
                $('#create-review').html(result);
                $('#create-review').show();
                console.log("GET /review/create/ Success!")
            },
            error: function (xhr) {
                console.log("GET /review/create/" + " Error: " + xhr.statusText);
            }
        });
    }

    //function createReview(userId) {
    //    var reviewObject = {
    //        "title": $('#create-review-title-input').val(),
    //        "body": $('#create-review-body-input').val(),
    //        "userId": userId
    //    };

    //    $.ajax({
    //        url: '/review/create/',
    //        data: JSON.stringify(reviewObject),
    //        type: 'POST',
    //        success: function (result) {
    //            console.log("POST /review/create/" + " Success!")
    //        },
    //        error: function (xhr) {
    //            console.log("POST /review/create/" + " Error: " + xhr.statusText);
    //        }
    //    });
    //}
});