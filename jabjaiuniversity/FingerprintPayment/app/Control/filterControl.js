(function ($) {
    $.fn.extend({
        filterControl: function (options) {
            var defaults = {
                level: true,
                level2: true,
                year: true,
                term: true
            };

            var options = $.extend(defaults, options);
            var el = this;
            var html = [];
            if (defaults.year && defaults.term) {
                html = [
                    '<div class="row">',
                          '<div class="col-xs-2">',
                              '<label class="pull-right">ปีการศึกษา : </label>',
                          '</div>',
                          '<div class="col-xs-4">',
                               '<select id="year" class="form-control">',
                                '<option value="">Loding...</option>',
                               '</select>',
                          '</div>',
                          '<div class="col-xs-1">',
                              '<label class="pull-right">เทอม : </label>',
                          '</div>',
                          '<div class="col-xs-4">',
                               '<select id="term" class="form-control">',
                                '<option value="">Loding...</option>',
                               '</select>',
                          '</div>',
                          '<div class="col-xs-1">',
                          '</div>',
                    '</div>'
                ]
            }
            if (defaults.level && defaults.level2) {
                html.push('<div class="row">',
                          '<div class="col-xs-2">',
                              '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
                          '</div>',
                          '<div class="col-xs-4">',
                              '<select name="selectlevel" class="form-control dropdown">',
                              '<option value="">Loding...</option>',
                              '</select>',
                          '</div>',
                          '<div class="col-xs-1">',
                              '<label class="pull-right">ชั้นเรียน :<span style="color: red;"></span></label>',
                          '</div>',
                          '<div class="col-xs-4">',
                              '<select name="selectsublevel" class="form-control">',
                              '<option value="">Loding...</option>',
                              '</select>',
                         '</div>',
                          '<div class="col-xs-1">',
                          '</div>',
                      '</div>');
            }
            else if (defaults.level) {
                html.push('<div class="row">',
                      '<div class="col-xs-2">',
                          '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
                      '</div>',
                      '<div class="col-xs-4">',
                          '<select name="selectlevel" class="form-control dropdown">',
                          '<option value="">Loding...</option>',
                          '</select>',
                        '</div>',
                        '<div class="col-xs-1">',
                        '</div>',
                        '<div class="col-xs-4">',
                        '</div>',
                        '<div class="col-xs-1">',
                        '</div>',
                    '</div>');
            }
            $(el).html(html.join("\n"));
        }
    })
})(jQuery);
