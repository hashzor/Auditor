function getWidth() {
    return Math.max(
      document.body.scrollWidth,
      document.documentElement.scrollWidth,
      document.body.offsetWidth,
      document.documentElement.offsetWidth,
      document.documentElement.clientWidth
    );
}

function getHeight() {
    return Math.max(
      document.body.scrollHeight,
      document.documentElement.scrollHeight,
      document.body.offsetHeight,
      document.documentElement.offsetHeight,
      document.documentElement.clientHeight
    );
}

function BodyLoad() {
    PreventBackspaceNavigations();
    $("#loaderBox").hide();
}

function BodyUnload() {
    $("#loaderBox").show();
}

function PreventBackspaceNavigations() {
    $(document).on('keydown', function (e) {
        var $target = $(e.target || e.srcElement);
        if (e.keyCode == 8 && !$target.is('input,[contenteditable="true"],textarea')) {
            e.preventDefault();
        }
    })
}

function AdjustGridViewHeight(s, e, offset) {
    var height = getHeight() - offset;
    s.SetHeight(height);
}