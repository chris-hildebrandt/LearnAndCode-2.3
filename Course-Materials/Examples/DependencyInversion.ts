// Bad Example of Dependency Inversion

/* In this bad example, MessageLogger directly depends on ConsoleLogger.
   This tightly couples MessageLogger to ConsoleLogger, making it hard to change the logging mechanism without modifying MessageLogger.
*/

class ConsoleLogger {
    log(message: string): void {
        console.log(message);
    }
}

class MessageLogger {
    private logger: ConsoleLogger;

    constructor(logger: ConsoleLogger) {
        this.logger = logger;
    }

    logMessage(message: string): void {
        this.logger.log(message);
    }
}

// Improved Example using Dependency Inversion Principle

/* Here, we introduce an ILogger interface and make ImprovedMessageLogger depend on this abstraction.
   This allows us to easily swap different logging implementations without modifying ImprovedMessageLogger.
*/

interface ILogger {
    log(message: string): void;
}

class ImprovedConsoleLogger implements ILogger {
    log(message: string): void {
        console.log(message);
    }
}

class ApiLogger implements ILogger {
    log(message: string): void {
        // Simulating an API call
        console.log(`Sending '${message}' to an API endpoint...`);
    }
}

class KafkaLogger implements ILogger {
    log(message: string): void {
        // Simulating a Kafka call
        console.log(`Sending '${message}' to a Kafka topic...`);
    }
}

class ImprovedMessageLogger {
    private logger: ILogger;

    constructor(logger: ILogger) {
        this.logger = logger;
    }

    logMessage(message: string): void {
        this.logger.log(message);
    }
}

// Usage examples
const consoleLoggerInstance = new ImprovedMessageLogger(new ConsoleLogger());
consoleLoggerInstance.logMessage("This logs to the console!");

const apiLoggerInstance = new ImprovedMessageLogger(new ApiLogger());
apiLoggerInstance.logMessage("This sends the message to an API!");

const kafkaLoggerInstance = new ImprovedMessageLogger(new KafkaLogger());
kafkaLoggerInstance.logMessage("This sends the message to Kafka!");