// tabs switcher code
function initTabs(){
	$('.tabset').each(function()
	{
		var _tablinks = $(this).find('a.tab');
		_tablinks.each(function(){
			_curtab = $(this).attr('href');
			if($(_curtab).length) {
				$(_curtab).addClass('inactive').removeClass('active').css('display', 'none');
				if($(this).hasClass('active')) $(_curtab).addClass('active').css('display', 'block');
			}

			$(this).hover(
				function(){
					_url = $(this).attr('href');
					_tablinks.each(function() {
						$($(this).attr('href')).removeClass("active").addClass("inactive").css({'display' : 'none', 'opacity' : '0'});
						$(this).removeClass('active');
						$(this).parent().removeClass("hover");
					});

					$(this).parent().addClass("hover");
					$(_url).removeClass("inactive").addClass("active").css({'display' : 'block', 'opacity' : '1'});
					$(this).addClass('active');
					return false;
				},
				function(){return false;}
			);
		});
	});
}

// side menu code
function initMenu() {
	$("ul.slide-menu li").each(function(){
		var _this = $(this);
		_this._sliding = false;

		$(this).hover(
			function() {
				if(_this._sliding) return false;
				_this._sliding = true;
				var _slider = $(this).find("div.slide").eq(0);
				if(_slider) {
					$(this).addClass("active")
					$(_slider).slideDown(300,function(){
						_sliding = false;
					});
				}
			},
			function() {
				_this = $(this);
				var _int;
				_int = setInterval(function(){
					if(!_this._sliding){
						clearTimeout(_int);
						var _slider = $(_this).find("div.slide").eq(0);
						if(_slider) {
							$(_this).removeClass("active")
							$(_slider).slideUp(300);
						}
					}
				},200);
			}
		);
	});
}

// page init code
$(document).ready(function(){
	initTabs();
	initMenu();
});