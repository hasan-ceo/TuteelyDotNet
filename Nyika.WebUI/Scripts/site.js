function ShowImagePreview(ImageUrl, previewImage) {
    if (ImageUrl.files && ImageUrl.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(ImageUrl.files[0]);
    }
}