
function Termfilter() {
    return $([
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
    ].join("\n"));
}