package org.example.test;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories; // 추가

@SpringBootApplication
@EnableJpaRepositories(basePackages = "org.example.test.repository") // ⬅️ UserRepository가 있는 실제 패키지 경로 입력!
public class TestApplication {
    public static void main(String[] args) {
        SpringApplication.run(TestApplication.class, args);
    }
}