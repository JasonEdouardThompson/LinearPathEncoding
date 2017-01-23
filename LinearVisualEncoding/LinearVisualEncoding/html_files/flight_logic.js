function reset() {
    currentState = STATE_SHOWING;
    duration = 0;

    if (timer != null
        && (currentState == STATE_ANIMATING_TO_NORMAL
            || currentState == STATE_ANIMATING_TO_SELECTION)) {

        clearInterval(timeOut);
    }

	var min_price = Number.MAX_VALUE;
	var max_price = 0;

	//reset
	$("#base > g[type='transfer']").each( function(){
		var price = parseFloat($(this).attr("price"));
		
		min_price = Math.min(price, min_price);
		max_price = Math.max(price, max_price);
	});
	
	$("#min_price").val(Math.floor(min_price).toFixed(2));
	$("#max_price").val(Math.ceil(max_price).toFixed(2));
	
	$("input[type='checkbox']").prop('checked', true);
	filter();
}

var mx = 0;
var my = 0;
	
function filter(){
	
	var min_price = parseFloat($("#min_price").val());
	var max_price = parseFloat($("#max_price").val());
	
	$("#up").empty();
	
	var airports = {};
	
	$("input[airport]").each( function(){
		airports[$(this).attr("airport")] = $(this).is(':checked');
	});
	
	$("#base > g[type='transfer']").each( function(){
		
		var price = parseFloat($(this).attr("price"));
		
		if( price < min_price 
				|| price > max_price 
				|| !airports[$(this).attr("source")]
				|| !airports[$(this).attr("destination")] ){
			$(this).clone().appendTo("#up").attr("opacity", 0.25).show().mouseover( showTooltip ).mouseout( hideTooltips );
			$(this).hide();
		}else{
			$(this).show();
		}
	});
}

var STATE_ANIMATING_TO_SELECTION = 0;
var STATE_ANIMATING_TO_NORMAL = 1;
var STATE_SHOWING = 2;
var selectedElement = null;
var timer = null;
var maxDuration = 3000;
var timeOut = 10;
var duration = 0;

var currentState = STATE_SHOWING;
var valueDecay = 0.05;
var minOpacity = 0.25;

function animation() {
    
    if( currentState == STATE_ANIMATING_TO_SELECTION ){
        
        if( duration > maxDuration ){
            selectedElement.attr("opacity", 1);
            $("g[type='transfer']").not(selectedElement).attr("opacity", minOpacity);
        }else{
            var opacity = parseFloat(selectedElement.attr("opacity"));
            selectedElement.attr("opacity", (1 - opacity) * valueDecay + opacity)
        
            $("g[type='transfer']").not(selectedElement).each(function(){
            
                var opacity = parseFloat($(this).attr("opacity"));

                $(this).attr("opacity", opacity + (minOpacity - opacity) * valueDecay);
            });
        }

    }else if(currentState == STATE_ANIMATING_TO_NORMAL ){

        if( duration > maxDuration ){
            $("g[type='transfer']").attr("opacity", 1);
        }else{
            
            $("#base > g[type='transfer']").each(function(){
            
                var opacity = parseFloat($(this).attr("opacity"));
            
                $(this).attr("opacity", (1 - opacity) * valueDecay + opacity)
            });

            $("#up > g[type='transfer']").each(function () {

                var opacity = parseFloat($(this).attr("opacity"));

                $(this).attr("opacity", opacity + (minOpacity - opacity) * valueDecay);
            });
        }
    }

    if( (duration += timeOut) < maxDuration ){
        timer = setTimeout(animation, timeOut);
    } else {
        currentState = STATE_SHOWING;
    }
}

function selectElement(el) {

    var oldState = currentState;

    currentState = STATE_ANIMATING_TO_SELECTION;
    duration = 0;
    selectedElement = el;

    if (oldState == STATE_SHOWING) {
        timer = setTimeout(animation, timeOut);
    }

}

function deselectAll() {

    var oldState = currentState;

    currentState = STATE_ANIMATING_TO_NORMAL;
    duration = 0;

    if (oldState == STATE_SHOWING) {
        timer = setTimeout(animation, timeOut);
    }

}

function showTooltip(){
	var startDate = new Date(parseInt($(this).attr("start")));
	var finishDate = new Date(parseInt($(this).attr("finish")));

	selectElement($(this))

	var content = "<em>" + $(this).attr("source")+"</em> to <em>" + $(this).attr("destination")+"</em>";
	content += "<br/> cost = $" + parseFloat($(this).attr("price")).toFixed(2);
	content += "<br/> departs = " + startDate.getUTCHours() + ":" + ("0" + startDate.getUTCMinutes()).slice(-2);
	content += "<br/> arrives = " + finishDate.getUTCHours() + ":" + ("0" + finishDate.getUTCMinutes()).slice(-2);

	$("body")
	.append("<div  style='top:"+my+"px;left:"+mx+"px;background:"+$(this).attr("colour")+";' class='tooltip'>"+content+"</div>");
}

function hideTooltips() {
    $(".tooltip").remove();
    deselectAll();
}


function focusOnEntity() {
    var el = $("g[source='" + $(this).attr("name") + "'],g[destination='" + $(this).attr("name") + "']");
    selectElement(el);
}


$(document).ready(function(){
		
	if (window.Event) {
		document.captureEvents(Event.MOUSEMOVE);
	}
	
	document.onmousemove = function(e){
		mx = (window.Event) ? e.pageX : e.clientX + (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
		my = (window.Event) ? e.pageY : e.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
	}
	
	reset();
	
	$("#reset").on("click", reset);
	$("#filter").on("click", filter);

	$("g[type='transfer']").attr("opacity", 1);

	$("#base > g[type='transfer']").mouseover(showTooltip).mouseout(hideTooltips);


	$("#base > g[type='entity']").mouseover(focusOnEntity).mouseout(deselectAll);
});

