
var hrefStates = {}

$(document).ready(function(){
	$( "g[type='transfer']" ).on( "click", clickTransfer);
	
	$( "g[type='transfer']" ).each( function(){
		hrefStates[$(this).attr("href")] = { "read" : false, "animating": false };
	});
	
	
	$( ".single-comment" ).on( "click", function(){
		// try and get it from the animation
		var href = $(this).attr("id");
		
		var visRep = $( "g[id='down'] > g[href='"+href+"']" )
		
		if( visRep.size() == 0 ){
			visRep = $( "g[id='base'] > g[href='"+href+"']" )
		}
		if( visRep.size() == 0 ){
			visRep = $( "g[id='up'] > g[href='"+href+"']" )
		}
		
		clickTransfer.call( visRep.get(0) );
	});
	
});

function deactivateAllAnimation(){
	for( var href in hrefStates ){
		hrefStates[href].animating = false;
	}
}

function clickTransfer() {
	
	//update the comment section on the right
	var href = $(this).attr("href")
	var item = $("#" + href )
	item.get(0).scrollIntoView();
	$(".single-comment").removeClass( "selected-comment");
	item.addClass( "selected-comment");
	
	var maxValue = 5;
	var duration = 250;
	
	var self = $(this)
	var original = self;
	
	function goUp(){
		if( !hrefStates[href].animating ){
			
			self.remove();
			
			if(hrefStates[href].read){
				$( "g[id='up'] > g[href='"+href+"']" ).attr("opacity", 0.5).show();
			}else{
				$( "g[id='base'] > g[href='"+href+"']" ).attr("opacity", 1).show();				
			}
			
			return;
		}
		self.animate(
			{'dx':-maxValue},
			{
				step: function(dx){
					 $(this).attr('transform', 'translate(' + dx + ' ' + dx + ')');
				},
				duration: duration,
				complete: goDown
			}
		);
	}
	
	function goDown(){
		self.animate(
			{'dx':0},
			{
				step: function(dx){
					 $(this).attr('transform', 'translate(' + dx + ' ' + dx + ')');
				},
				duration: duration,
				complete: goUp
			}
		);			
	}
	
	if( !hrefStates[href].animating ){
		
		self = $( this )
					.clone()
					.appendTo( "g[id='down']" )
					.on("click", clickTransfer);
					
		original.hide();
		
		deactivateAllAnimation();
		hrefStates[href].animating = true;
		
		goUp();
	}else{
		deactivateAllAnimation();		
		$(".single-comment").removeClass( "selected-comment");
	}
}



function toggleComment(sender, href, newValue){
	
	if( newValue == hrefStates[href].read){
		return;
	}
	
	if(hrefStates[href].read){//change to unread
	
		$( "g[id='up'] > g[href='"+href+"']" ).remove();	
		$("#"+href).removeClass("read-comment");
		
		if( hrefStates[href].animating ){
			$( "g[id='down'] > g[href='"+href+"']" ).attr("opacity", 1);
		}else{
			$( "g[id='base'] > g[href='"+href+"']" ).show();
		}
		sender.innerHTML = "hide comment"
		
	}else{//change to read
		sender.innerHTML = "show comment"
	
		var newRead = $( "g[id='base'] > g[href='"+href+"']" )
						.clone()
						.appendTo( "g[id='up']" )
						.attr("opacity", 0.5)
						.on("click", clickTransfer);

		$( "g[id='base'] > g[href='"+href+"']" ).hide();
		$("#"+href).addClass("read-comment");
		
		if( hrefStates[href].animating ){
			$( "g[id='down'] > g[href='"+href+"']" ).attr("opacity", 0.5);
			newRead.hide();
		}else{
			newRead.show();
		}
	}
	hrefStates[href].read = !hrefStates[href].read;
}

function toggleThread(sender, href){
	
	var visRepsUp = $( "g[id='up'] > g[thread='"+href+"']" );
	var visRepBase = $( "g[id='base'] > g[thread='"+href+"']" );
	
	var makeVisible = (visRepsUp.size() > 0);
	
	visRepBase.each( function(){
		toggleComment($("button[href='"+$(this).attr("href")+"']").get(0), $(this).attr("href"), !makeVisible);
	});
}

