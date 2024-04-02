package com.example.bestesttheaters.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/v1")
public class ApiController {
	@GetMapping("/shows")
	public ShowsDto listShows() {
		return new ShowsDto(List.of());
	}

}