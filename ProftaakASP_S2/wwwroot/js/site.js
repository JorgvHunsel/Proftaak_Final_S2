// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
function setStarRating(ratingNumber) {
    var starRating = document.getElementById('starRating');
    for (var i = 0; i < starRating.children.length; i++) {
        var starRatingChild = starRating.children[i];
        for (var starPart = 0; starPart < starRatingChild.children.length; starPart++) {
                if (starRatingChild.children[starPart].children[0].id <= ratingNumber) {
                    starRatingChild.children[starPart].children[0].style.fill = "goldenrod";
                }
                else {
                    starRatingChild.children[starPart].children[0].style.fill = "";
                    starRatingChild.children[starPart].classList.remove("text-warning");
                }
            }
        }
        var ratingId = document.getElementById('starAmount');
        ratingId.value = ratingNumber;
    }