# nexusgo-signsupportdemo

## Start Support service Docker container
```
docker run --interactive --volume c:/sign-support/test/files/:/home/docker/additional/:ro \
--env APPLICATION_SIGNSUPPORT_SIGNING_KEY=file:/home/docker/additional/signing-key.pem \
--env APPLICATION_SIGNSUPPORT_SIGNING_KEY_PASSWORD=******* \
--env APPLICATION_SIGNSUPPORT_SIGNING_CERTIFICATE=file:/home/docker/additional/signing-certificate.pem \
--env APPLICATION_SIGNSUPPORT_VALIDATION_CERTIFICATE=file:/home/docker/additional/validation-certificate.cer \
--env APPLICATION_SIGNSUPPORT_PROFILE_CONFIG=file:/home/docker/additional/profile.json \
--env APPLICATION_SIGNSUPPORT_ENTITIES_DESCRIPTOR=file:/home/docker/additional/entities-descriptor.xml \
--publish 8080:8080 technologynexus/sign-support
```
