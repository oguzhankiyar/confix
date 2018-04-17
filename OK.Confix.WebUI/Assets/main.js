$(function () {
    $.each($('[data-showmodal]'), function (index, item) {
        var $item = $(item);

        $item.on('click', function () {
            var $modal = $('#baseModal'),
                $content = $modal.find('.modal-content');

            $content.html('<div class="text-center" style="margin: 20px 0;">Yükleniyor...</div>');

            $modal.modal('show');

            window.setTimeout(function () {
                $content.load($item.data('showmodal'));
            }, 1000);
        });
    });
});