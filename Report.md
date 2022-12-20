# Report

## Assumptions

* system shall fail in case of a score value overflow.
* reward groups shall be interpreted sequentially in the order that they are read, similar to how one would expect to see rewards in a game as they progress through the levels of diffculty.

## Code localisation

For each supported language, there will be a *.json file defined. Expectation is that different files for different languages contain the same groups with rewards, in the same order, only the strings differ.

Given that the current group and next reward are kept track of by means of indexes, one could define a method on the existing `User` class, `UpdateRewardGroups(IList<RewardGroup> groups)`, which would update the underlying collection of reward groups. This way, switching between different languages can be done during runtime, without affecting the current logic flow.

## Scale factor

There are two possible approaches I would consider:

1. Apply the scale factor directly on the currently calculated score value, which in turn would result in advancing the user through the remaining groups of rewards.
2. Keep a secondary "Bonus score" and scale that bsaed on the current main score value. Could provide access to bonus levels, which a user may choose to pursue or not.
