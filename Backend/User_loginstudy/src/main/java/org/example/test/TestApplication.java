package org.example.test;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.persistence.autoconfigure.EntityScan;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@SpringBootApplication
@ComponentScan(basePackages = {"org.example.test", "com.example.userapi"})
@EnableJpaRepositories(basePackages = "com.example.userapi.repository")
@EntityScan(basePackages = "com.example.userapi.entity")
public class TestApplication {

    public static void main(String[] args) {SpringApplication.run(TestApplication.class, args);
    }

}
